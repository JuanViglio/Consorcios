﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.18408
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::System.Data.Objects.DataClasses.EdmSchemaAttribute()]

// Nombre de archivo original:
// Fecha de generación: 26/03/2017 12:22:54 p.m.
namespace DAO
{
    
    /// <summary>
    /// No hay ningún comentario para ExpensasEntities en el esquema.
    /// </summary>
    public partial class ExpensasEntities : global::System.Data.Objects.ObjectContext
    {
        /// <summary>
        /// Inicializa un nuevo objeto ExpensasEntities usando la cadena de conexión encontrada en la sección 'ExpensasEntities' del archivo de configuración de la aplicación.
        /// </summary>
        public ExpensasEntities() : 
                base("name=ExpensasEntities", "ExpensasEntities")
        {
            this.OnContextCreated();
        }
        /// <summary>
        /// Inicializar un nuevo objeto ExpensasEntities.
        /// </summary>
        public ExpensasEntities(string connectionString) : 
                base(connectionString, "ExpensasEntities")
        {
            this.OnContextCreated();
        }
        /// <summary>
        /// Inicializar un nuevo objeto ExpensasEntities.
        /// </summary>
        public ExpensasEntities(global::System.Data.EntityClient.EntityConnection connection) : 
                base(connection, "ExpensasEntities")
        {
            this.OnContextCreated();
        }
        partial void OnContextCreated();
        /// <summary>
        /// No hay ningún comentario para Consorcios en el esquema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public global::System.Data.Objects.ObjectQuery<Consorcio> Consorcios
        {
            get
            {
                if ((this._Consorcios == null))
                {
                    this._Consorcios = base.CreateQuery<Consorcio>("[Consorcios]");
                }
                return this._Consorcios;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private global::System.Data.Objects.ObjectQuery<Consorcio> _Consorcios;
        /// <summary>
        /// No hay ningún comentario para Consorcios en el esquema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public void AddToConsorcios(Consorcio consorcio)
        {
            base.AddObject("Consorcios", consorcio);
        }
    }
    /// <summary>
    /// No hay ningún comentario para ExpensasModel.Consorcio en el esquema.
    /// </summary>
    /// <KeyProperties>
    /// ID
    /// </KeyProperties>
    [global::System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName="ExpensasModel", Name="Consorcio")]
    [global::System.Runtime.Serialization.DataContractAttribute(IsReference=true)]
    [global::System.Serializable()]
    public partial class Consorcio : global::System.Data.Objects.DataClasses.EntityObject
    {
        /// <summary>
        /// Crear un nuevo objeto Consorcio.
        /// </summary>
        /// <param name="id">Valor inicial de ID.</param>
        /// <param name="direccion">Valor inicial de Direccion.</param>
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public static Consorcio CreateConsorcio(string id, string direccion)
        {
            Consorcio consorcio = new Consorcio();
            consorcio.ID = id;
            consorcio.Direccion = direccion;
            return consorcio;
        }
        /// <summary>
        /// No hay ningún comentario para la propiedad ID en el esquema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public string ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this.OnIDChanging(value);
                this.ReportPropertyChanging("ID");
                this._ID = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ID");
                this.OnIDChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private string _ID;
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnIDChanging(string value);
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnIDChanged();
        /// <summary>
        /// No hay ningún comentario para la propiedad Direccion en el esquema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public string Direccion
        {
            get
            {
                return this._Direccion;
            }
            set
            {
                this.OnDireccionChanging(value);
                this.ReportPropertyChanging("Direccion");
                this._Direccion = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Direccion");
                this.OnDireccionChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private string _Direccion;
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnDireccionChanging(string value);
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnDireccionChanged();
        /// <summary>
        /// No hay ningún comentario para la propiedad UltimaExpensa en el esquema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        public string UltimaExpensa
        {
            get
            {
                return this._UltimaExpensa;
            }
            set
            {
                this.OnUltimaExpensaChanging(value);
                this.ReportPropertyChanging("UltimaExpensa");
                this._UltimaExpensa = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("UltimaExpensa");
                this.OnUltimaExpensaChanged();
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        private string _UltimaExpensa;
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnUltimaExpensaChanging(string value);
        [global::System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")]
        partial void OnUltimaExpensaChanged();
    }
}
