namespace BasarSoftProject.Repositories
{
    using Npgsql;
    using NetTopologySuite.IO;
    using NetTopologySuite.Geometries;

    public class AdoNetHelper
    {
        private readonly string _connectionString;
        public AdoNetHelper(string connectionString) => _connectionString = connectionString;

        #region Execute (Insert, Update, Delete)
        public async Task<int> ExecuteAsync(string sql, Dictionary<string, object> parameters)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                using var cmd = new NpgsqlCommand(sql, conn);
                foreach (var p in parameters)
                    cmd.Parameters.AddWithValue(p.Key, p.Value ?? DBNull.Value);

                return await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                // Loglama ekleyebilirsin
                throw new Exception("ExecuteAsync Hatası: " + ex.Message, ex);
            }
        }
        #endregion

        #region Query (Select)
        public async Task<List<Dictionary<string, object>>> QueryAsync(string sql, Dictionary<string, object> parameters)
        {
            var result = new List<Dictionary<string, object>>();

            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                using var cmd = new NpgsqlCommand(sql, conn);
                foreach (var p in parameters)
                    cmd.Parameters.AddWithValue(p.Key, p.Value ?? DBNull.Value);

                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var row = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                        row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                    result.Add(row);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("QueryAsync Hatası: " + ex.Message, ex);
            }

            return result;
        }
        #endregion

        #region Transaction
        public async Task<int> ExecuteTransactionAsync(List<(string sql, Dictionary<string, object> parameters)> commands)
        {
            int totalAffected = 0;

            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            using var transaction = await conn.BeginTransactionAsync();

            try
            {
                foreach (var cmdItem in commands)
                {
                    using var cmd = new NpgsqlCommand(cmdItem.sql, conn, transaction);
                    foreach (var p in cmdItem.parameters)
                        cmd.Parameters.AddWithValue(p.Key, p.Value ?? DBNull.Value);

                    totalAffected += await cmd.ExecuteNonQueryAsync();
                }

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Transaction Hatası: " + ex.Message, ex);
            }

            return totalAffected;
        }
        #endregion

        #region PostGIS / WKT Yardımcıları
        public string ConvertWktToSqlGeometry(string wkt, int srid = 4326)
        {
            // PostGIS SQL: ST_GeomFromText('LINESTRING(0 0,1 1)', 4326)
            return $"ST_GeomFromText('{wkt}', {srid})";
        }

        public Geometry ParseWkt(string wkt)
        {
            try
            {
                var reader = new WKTReader();
                return reader.Read(wkt);
            }
            catch (Exception ex)
            {
                throw new Exception("WKT Parsing Hatası: " + ex.Message, ex);
            }
        }
        #endregion
    }

}
