// ------------------------------------------------------------------------------
// <auto-generated>
//    Generated by avrogen, version 1.12.0+8c27801dc8d42ccc00997f25c0b8f45f8d4a233e
//    Changes to this file may cause incorrect behavior and will be lost if code
//    is regenerated
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Contracts
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using global::Avro;
	using global::Avro.Specific;
	
	[global::System.CodeDom.Compiler.GeneratedCodeAttribute("avrogen", "1.12.0+8c27801dc8d42ccc00997f25c0b8f45f8d4a233e")]
	public partial class Dish : global::Avro.Specific.ISpecificRecord
	{
		public static global::Avro.Schema _SCHEMA = global::Avro.Schema.Parse("{\"type\":\"record\",\"name\":\"Dish\",\"namespace\":\"Contracts\",\"fields\":[{\"name\":\"Cost\",\"" +
				"type\":{\"type\":\"bytes\",\"logicalType\":\"decimal\",\"precision\":29,\"scale\":2}},{\"name" +
				"\":\"Id\",\"type\":{\"type\":\"string\",\"logicalType\":\"uuid\"}},{\"name\":\"Title\",\"type\":\"st" +
				"ring\"}]}");
		private Avro.AvroDecimal _Cost;
		private System.Guid _Id;
		private string _Title;
		public virtual global::Avro.Schema Schema
		{
			get
			{
				return Dish._SCHEMA;
			}
		}
		public Avro.AvroDecimal Cost
		{
			get
			{
				return this._Cost;
			}
			set
			{
				this._Cost = value;
			}
		}
		public System.Guid Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
			}
		}
		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				this._Title = value;
			}
		}
		public virtual object Get(int fieldPos)
		{
			switch (fieldPos)
			{
			case 0: return this.Cost;
			case 1: return this.Id;
			case 2: return this.Title;
			default: throw new global::Avro.AvroRuntimeException("Bad index " + fieldPos + " in Get()");
			};
		}
		public virtual void Put(int fieldPos, object fieldValue)
		{
			switch (fieldPos)
			{
			case 0: this.Cost = (Avro.AvroDecimal)fieldValue; break;
			case 1: this.Id = (System.Guid)fieldValue; break;
			case 2: this.Title = (System.String)fieldValue; break;
			default: throw new global::Avro.AvroRuntimeException("Bad index " + fieldPos + " in Put()");
			};
		}
	}
}
