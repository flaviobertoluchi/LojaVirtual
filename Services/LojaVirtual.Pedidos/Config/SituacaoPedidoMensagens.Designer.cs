﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LojaVirtual.Pedidos.Config {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class SituacaoPedidoMensagens {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SituacaoPedidoMensagens() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("LojaVirtual.Pedidos.Config.SituacaoPedidoMensagens", typeof(SituacaoPedidoMensagens).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Pagamento recebido, aguardando envio..
        /// </summary>
        public static string Aprovado {
            get {
                return ResourceManager.GetString("Aprovado", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Pedido cancelado, o pagamento foi recusado..
        /// </summary>
        public static string Cancelado {
            get {
                return ResourceManager.GetString("Cancelado", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Pedido entregue..
        /// </summary>
        public static string Entregue {
            get {
                return ResourceManager.GetString("Entregue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Pedido enviado..
        /// </summary>
        public static string Enviado {
            get {
                return ResourceManager.GetString("Enviado", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Pedido finalizado..
        /// </summary>
        public static string Finalizado {
            get {
                return ResourceManager.GetString("Finalizado", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Pedido recebido com sucesso, aguardando aprovação do pagamento..
        /// </summary>
        public static string Recebido {
            get {
                return ResourceManager.GetString("Recebido", resourceCulture);
            }
        }
    }
}
