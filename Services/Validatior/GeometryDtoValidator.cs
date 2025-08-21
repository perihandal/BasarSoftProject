using App.Core.DTOs;
using App.Core.Entities;
using App.Services.Resources;
using NetTopologySuite.IO;
using System;

namespace App.Core.Validators
{
    public static class GeometryValidator
    {
        public static string? Validate(GeometryDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return ErrorMessages.NameRequierd;

            if (string.IsNullOrWhiteSpace(dto.Wkt))
                return ErrorMessages.WktRequired;

            if (!Enum.TryParse<App.Core.Entities.GeometryType>(dto.Type, true, out _))
                return ErrorMessages.TypeRequired;

            try
            {
                var reader = new WKTReader();
                reader.Read(dto.Wkt);
            }
            catch
            {
                return ErrorMessages.InvalidWkt;
            }

            return null;
        }
    }
}
