﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Athenas.EjemploTemplateApi.Application.Resources {
    using System;
    
    
    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile su proyecto de VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class StringManager {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal StringManager() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Athenas.EjemploTemplateApi.Application.Resources.StringManager", typeof(StringManager).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Reemplaza la propiedad CurrentUICulture del subproceso actual para todas las
        ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Invalid request parameters..
        /// </summary>
        internal static string BADREQUEST_MESSAGE {
            get {
                return ResourceManager.GetString("BADREQUEST_MESSAGE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Error en la llamada al obtener token.
        /// </summary>
        internal static string CALL_FAILED_TOKEN {
            get {
                return ResourceManager.GetString("CALL_FAILED_TOKEN", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Una o mas validaciones han fallado.
        /// </summary>
        internal static string FAILED_VALIDATIONS {
            get {
                return ResourceManager.GetString("FAILED_VALIDATIONS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Excepción no controlada para solicitud {Name} {@Request}.
        /// </summary>
        internal static string UNHANDLED_EXCEPTION {
            get {
                return ResourceManager.GetString("UNHANDLED_EXCEPTION", resourceCulture);
            }
        }
    }
}