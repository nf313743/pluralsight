﻿//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.7.0.0 (NJsonSchema v10.1.24.0 (Newtonsoft.Json v12.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

#pragma warning disable 108 // Disable "CS0108 '{derivedDto}.ToJson()' hides inherited member '{dtoBase}.ToJson()'. Use the new keyword if hiding was intended."
#pragma warning disable 114 // Disable "CS0114 '{derivedDto}.RaisePropertyChanged(String)' hides inherited member 'dtoBase.RaisePropertyChanged(String)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword."
#pragma warning disable 472 // Disable "CS0472 The result of the expression is always 'false' since a value of type 'Int32' is never equal to 'null' of type 'Int32?'
#pragma warning disable 1573 // Disable "CS1573 Parameter '...' has no matching param tag in the XML comment for ...
#pragma warning disable 1591 // Disable "CS1591 Missing XML comment for publicly visible type or member ..."

namespace Movies.Client.Models
{
    using System = global::System;

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.1.24.0 (Newtonsoft.Json v12.0.0.0)")]
    public partial class MovieForCreation
    {
        [Newtonsoft.Json.JsonProperty("title", Required = Newtonsoft.Json.Required.Always)]
        public string Title { get; set; }

        [Newtonsoft.Json.JsonProperty("description", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Description { get; set; }

        [Newtonsoft.Json.JsonProperty("genre", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Genre { get; set; }

        [Newtonsoft.Json.JsonProperty("releaseDate", Required = Newtonsoft.Json.Required.Always)]
        public System.DateTimeOffset ReleaseDate { get; set; }

        [Newtonsoft.Json.JsonProperty("directorId", Required = Newtonsoft.Json.Required.Always)]
        public System.Guid DirectorId { get; set; }


    }

}

#pragma warning restore 1591
#pragma warning restore 1573
#pragma warning restore 472
#pragma warning restore 114
#pragma warning restore 108