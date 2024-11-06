namespace Athenas.EjemploTemplateApi.WebApi.Helpers
{
    /// <summary>
    /// Valores de configuracion Swagger
    /// </summary>
    public static class SwaggerConfiguration
    {
        /// <summary>
        /// Descripción que aparece en el menú desplegable del selector de documentos.
        /// </summary>
        public const string EndpointDescription = "WebApi";
        /// <summary>
        /// Url archivo oas
        /// </summary>
        public const string EndPointUrl = "/swagger/v1/swagger.json";
        /// <summary>
        /// Url archivo oas en desarrollo
        /// </summary>
        public const string EndPointEnviromentUrl = "/swagger/v1/swagger.json";
        /// <summary>
        /// Titulo de la aplicación
        /// </summary>
        public const string DocInfoTitle = "Titulo - Producto";
        /// <summary>
        /// Versión del documento OpenApi
        /// </summary>
        public const string DocInfoVersion = "v1";
        /// <summary>
        /// Descripción corta de la aplicacion
        /// </summary>
        public const string DocInfoDescription = "Descripcion - WebApi";
    }
}
