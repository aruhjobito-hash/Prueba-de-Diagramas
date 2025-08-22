using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ApiAppLeon.Models.Desarrollo;

namespace ApiAppLeon.Controllers
{
    [ApiExplorerSettings(GroupName = "Desarrollo")]
    [Route("api/[controller]")]
    [ApiController]
    public class CrearServicioController : ControllerBase
    {
        [HttpPost("CreateFiles")]
        public IActionResult CreateFiles([FromBody] FileGenerationRequest request)
        {
            try
            {
                // Validamos que todos los campos necesarios hayan sido ingresados
                if (request == null || string.IsNullOrEmpty(request.Workfolder) || string.IsNullOrEmpty(request.NameController) || string.IsNullOrEmpty(request.NameModel) || string.IsNullOrEmpty(request.Fields))
                {
                    return BadRequest("Faltan campos obligatorios en el request.");
                }
                bool CrearDir;
                // Creamos la ruta en la que van a ser creados los archivos y nos aseguramos que no contenga doble // para evitar errores en la dirección obtenida por el metodo Path
                string controllerFolder = Path.Combine("Controllers", request.Workfolder);
                controllerFolder = controllerFolder.Replace(@"\\", @"\");
                string modelFolder = Path.Combine("Models", request.Workfolder);
                modelFolder = modelFolder.Replace(@"\\", @"\");
                string[] workfolderParts = request.Workfolder.Split(@"\");
                if (!Directory.Exists(controllerFolder))

                    Directory.CreateDirectory(controllerFolder);
                if (!Directory.Exists(workfolderParts[0])) {

                    CrearDir = false;
                }else
                {
                    CrearDir= true;
                }
                    

                if (!Directory.Exists(modelFolder))
                    Directory.CreateDirectory(modelFolder);


                // Generamos el contenido del modelo
                string modelContent = GenerateModelContent(request);
                string modelFilePath = Path.Combine(modelFolder, $"{request.NameModel}Model.cs");
                System.IO.File.WriteAllText(modelFilePath, modelContent);

                // Generamos el contenido del controlador
                string controllerContent = GenerateControllerContent(request);
                string controllerFilePath = Path.Combine(controllerFolder, $"{request.NameController}Controller.cs");
                System.IO.File.WriteAllText(controllerFilePath, controllerContent);

                // En caso se especifique que existe un modelo BD diferente del request se especifica
                //if (request.DbModel == 1 || request.DbModel == 0)
                //{
                //    string dbModelContent = GenerateDbModelContent(request);
                //    string dbModelFilePath = Path.Combine(modelFolder, $"{request.NameModel}DBModel.cs");
                //    System.IO.File.WriteAllText(dbModelFilePath, dbModelContent);
                //}
                DbContextFormat dbContextFormat = new DbContextFormat();
                if ( CrearDir)
                { 
                    dbContextFormat.Navegador =  $"new SwaggerGroupConfig {{ Name = \"{workfolderParts[0]}\", Description = \"Agrupacion para endpoints de {workfolderParts[1]}\" }}";

                    //new SwaggerGroupConfig { Name = "Utilitarios", Description = "Carpeta para endpoints de Utilitarios" }
                }
                else
                {
                    dbContextFormat.Navegador = "";
                }
                dbContextFormat.Variable = $"public virtual DbSet<{request.NameModel + "DBModel"}> {request.NameModel + "DB"} {{ get; set; }} = null!;";
                dbContextFormat.Funcion = $"modelBuilder.Entity<{request.NameModel}DBModel>().HasNoKey();";

                //return Ok(new { message = "Files created successfully" });
                return Ok(dbContextFormat);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        private string GenerateControllerContent(FileGenerationRequest request)
        {
            string[] workfolderParts = request.Workfolder.Split('/');
            string requestWorkfolderRef = workfolderParts[0];
            var properties = request.Params.Split(',');
            var modelProperties = new List<string>();
            string modelfield;
            foreach (var field in properties)
            {
                var name = field.Trim().Split(' ')[0];
                modelProperties.Add($"\"{name}\"");
            }
            string result = string.Join(",", modelProperties);
            string controllerTemplate = $@"
// Developer    : {request.NameDeveloper}  
// DateCreate   : {DateTime.Now:dd/MM/yyyy}
// Description  : {request.Comments}
using Microsoft.AspNetCore.Mvc;
using ApiAppLeon.Models.{requestWorkfolderRef};
using Microsoft.EntityFrameworkCore;
using ApiAppLeon.Models.Sistemas;
using ApiAppLeon;
using Microsoft.Data.SqlClient;

namespace {requestWorkfolderRef}.Controllers
{{
    [ApiExplorerSettings(GroupName = ""{request.Workfolder}"")]
    [Route(""api/{request.Workfolder}/[controller]"")]
    [ApiController]
    public class {request.NameController}Controller : ControllerBase
    {{
        private readonly DBContext _dbContext;

        public {request.NameController}Controller(DBContext dbContext)
        {{
            _dbContext = dbContext;
        }}

        // POST: api/{request.NameController}
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<{request.NameModel}DBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost]
        public async Task<ActionResult<Respuesta<request{request.NameModel}Model>>> Post{request.NameModel}([FromBody] request{request.NameModel}Model model)
        {{
            if (!ModelState.IsValid)
            {{
                return BadRequest(new Respuesta<ErrorTxA> {{ Exito = 0, Mensaje = ""Invalid Model State"", Data = new ErrorTxA {{ codigo = ""01"", Mensaje = ""Model state validation failed"" }} }});
            }}
            try
            {{
                //1.- Definimos los parametros que entrarán al store procedure
                var parameterNames = new[]
                {{
                    {result}
                }};
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name => new SqlParameter(""@"" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)).ToArray();
                //3.- Creamos el store procedure que será llamado
                string store{request.NameController} = $""{request.NameProcedure} {{string.Join("", "", parameterNames.Select(n => ""@"" + n))}}"";
                //4.- Almacenamos la información obtenida del store procedure
                List<{request.NameModel + "DBModel"}> producto = await _dbContext.{request.NameModel + "DB"}.FromSqlRaw(store{request.NameController}, parameters).ToListAsync();
                
                if (producto != null && producto.Count > 0)
                {{
                    return Ok(new Respuesta<List<{request.NameModel + "DBModel"}>>{{ Exito = 1, Mensaje = ""Success"",Data = producto }});
                }}
                else
                {{
                    return NotFound(new Respuesta<ErrorTxA> {{ Exito = 0, Mensaje = ""No existen registros"", Data = new ErrorTxA {{ codigo = ""02"", Mensaje = ""No se obtuvo datos del la base de datos"" }} }});
                }}
            }}
            catch (Exception ex)
            {{
                return StatusCode(500, new Respuesta<ErrorTxA> {{ Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA {{ codigo = ""03"", Mensaje = ""Internal Server Error"" }} }});
            }}
        }}

        // PUT: api/{request.NameController}/{{id}}        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<{request.NameModel}DBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> Put{request.NameModel}(int id, [FromBody] request{request.NameModel}Model model)
        {{
            if (!ModelState.IsValid)
            {{
                return BadRequest(new Respuesta<ErrorTxA> {{ Exito = 0, Mensaje = ""Invalid Model State"", Data = new ErrorTxA {{ codigo = ""01"", Mensaje = ""Model state validation failed"" }} }});
            }}
            try
            {{
                //1.- Definimos los parametros que entrarán al store procedure
                var parameterNames = new[]
                {{
                    {result}
                }};
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name => new SqlParameter(""@"" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)).ToArray();
                //3.- Creamos el store procedure que será llamado
                string store{request.NameController} = $""{request.NameProcedure} {{string.Join("", "", parameterNames.Select(n => ""@"" + n))}}"";
                //4.- Almacenamos la información obtenida del store procedure
                List<{request.NameModel + "DBModel"}> producto = await _dbContext.{request.NameModel + "DB"}.FromSqlRaw(store{request.NameController}, parameters).ToListAsync();
                
                if (producto != null && producto.Count > 0)
                {{
                    return Ok(new Respuesta<List<{request.NameModel + "DBModel"}>>{{ Exito = 1, Mensaje = ""Success"",Data = producto }});
                }}
                else
                {{
                    return NotFound(new Respuesta<ErrorTxA> {{ Exito = 0, Mensaje = ""No existen registros"", Data = new ErrorTxA {{ codigo = ""02"", Mensaje = ""No se obtuvo datos del la base de datos"" }} }});
                }}
            }}
            catch (Exception ex)
            {{
                return StatusCode(500, new Respuesta<ErrorTxA> {{ Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA {{ codigo = ""03"", Mensaje = ""Internal Server Error"" }} }});
            }}
        }}

        // GET: api/{request.NameController}
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<{request.NameModel}DBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<request{request.NameModel}Model>>> Get{request.NameModel}Model(int id, [FromBody] request{request.NameModel}Model model)
        {{
            if (!ModelState.IsValid)
            {{
                return BadRequest(new Respuesta<ErrorTxA> {{ Exito = 0, Mensaje = ""Invalid Model State"", Data = new ErrorTxA {{ codigo = ""01"", Mensaje = ""Model state validation failed"" }} }});
            }}
            try
            {{
                //1.- Definimos los parametros que entrarán al store procedure
                var parameterNames = new[]
                {{
                    {result}
                }};
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name => new SqlParameter(""@"" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)).ToArray();
                //3.- Creamos el store procedure que será llamado
                string store{request.NameController} = $""{request.NameProcedure} {{string.Join("", "", parameterNames.Select(n => ""@"" + n))}}"";
                //4.- Almacenamos la información obtenida del store procedure
                List<{request.NameModel + "DBModel"}> producto = await _dbContext.{request.NameModel + "DB"}.FromSqlRaw(store{request.NameController}, parameters).ToListAsync();
                
                if (producto != null && producto.Count > 0)
                {{
                    return Ok(new Respuesta<List<{request.NameModel + "DBModel"}>>{{ Exito = 1, Mensaje = ""Success"",Data = producto }});
                }}
                else
                {{
                    return NotFound(new Respuesta<ErrorTxA> {{ Exito = 0, Mensaje = ""No existen registros"", Data = new ErrorTxA {{ codigo = ""02"", Mensaje = ""No se obtuvo datos del la base de datos"" }} }});
                }}
            }}
            catch (Exception ex)
            {{
                return StatusCode(500, new Respuesta<ErrorTxA> {{ Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA {{ codigo = ""03"", Mensaje = ""Internal Server Error"" }} }});
            }}
        }}

        // DELETE: api/{request.NameController}/{{id}}
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<{request.NameModel}DBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> Delete{request.NameModel}(int id, [FromBody] request{request.NameModel}Model model)
        {{
            if (!ModelState.IsValid)
            {{
                return BadRequest(new Respuesta<ErrorTxA> {{ Exito = 0, Mensaje = ""Invalid Model State"", Data = new ErrorTxA {{ codigo = ""01"", Mensaje = ""Model state validation failed"" }} }});
            }}
            try
            {{                
                //1.- Definimos los parametros que entrarán al store procedure
                var parameterNames = new[]
                {{
                    {result}
                }};
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name => new SqlParameter(""@"" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)).ToArray();
                //3.- Creamos el store procedure que será llamado
                string store{request.NameController} = $""{request.NameProcedure} {{string.Join("", "", parameterNames.Select(n => ""@"" + n))}}"";
                //4.- Almacenamos la información obtenida del store procedure
                List<{request.NameModel + "DBModel"}> producto = await _dbContext.{request.NameModel + "DB"}.FromSqlRaw(store{request.NameController}, parameters).ToListAsync();
                
                if (producto != null && producto.Count > 0)
                {{
                    return Ok(new Respuesta<List<{request.NameModel + "DBModel"}>>{{ Exito = 1, Mensaje = ""Success"",Data = producto }});
                }}
                else
                {{
                    return NotFound(new Respuesta<ErrorTxA> {{ Exito = 0, Mensaje = ""No existen registros"", Data = new ErrorTxA {{ codigo = ""02"", Mensaje = ""No se obtuvo datos del la base de datos"" }} }});
                }}
            }}
            catch (Exception ex)
            {{
                return StatusCode(500, new Respuesta<ErrorTxA> {{ Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA {{ codigo = ""03"", Mensaje = ""Internal Server Error"" }} }});
            }}
        }}

    }}
}}";

            return controllerTemplate;
        }
        private string GenerateDbModelContent(FileGenerationRequest request)
        {
            string[] workfolderParts = request.Workfolder.Split('/');
            string requestWorkfolderRef = workfolderParts[0];
            // Based on the DbModel value, we choose which set of fields to use (Params or Fields)
            string modelFields = request.DbModel == 1 ? request.Params : request.Fields;

            // Generate the DB Model class template
            string dbModelTemplate = $@"
// Developer: {request.NameDeveloper} {DateTime.Now:dd/MM/yyyy} - {request.Comments}

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.{requestWorkfolderRef}
{{
    public class {request.NameModel}DBModel
    {{
        {GenerateModelProperties(modelFields)}
    }}
}}";

            return dbModelTemplate;
        }

        private string GenerateModelContent(FileGenerationRequest request)
        {
            string[] workfolderParts = request.Workfolder.Split('/');
            string requestWorkfolderRef = workfolderParts[0];
            string modelTemplate = $@"
// Developer: {request.NameDeveloper} {DateTime.Now:dd/MM/yyyy} - {request.Comments}
// DateCreate   : {DateTime.Now:dd/MM/yyyy}

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.{requestWorkfolderRef}
{{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: {request.NameController} 
    /// </summary>
    public class request{request.NameModel}Model
    {{
        public int? Id {{ get; set; }}
        {GenerateModelProperties(request.Fields)}
    }}
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_{request.NameController} 
    /// </summary>
    [Keyless]
    public class {request.NameModel}DBModel
    {{
        {GenerateModelProperties(request.DbModel == 1 ? request.Params : request.Fields)}
    }}
    // De requerir mas modelos o ser distintos agregar abajo de esta linea siguiendo la siguiente estructura
    // [Keyless]
    // public class NombreModel
    // {{
    //    public int? Id {{ get; set; }}
    //    /*Demás tipos de datos para el nuevo modelo*/
    // }}
}}";

            return modelTemplate;
        }

        private string GenerateModelProperties(string fields)
        {
            if (string.IsNullOrWhiteSpace(fields))
            {
                throw new ArgumentException("Fields cannot be null or empty.", nameof(fields));
            }

            var properties = fields.Split(',');
            var modelProperties = new List<string>();

            foreach (var field in properties)
            {
                var fieldParts = field.Trim().Split(' ');

                // Check if the field is correctly formatted (at least two components: name and type)
                if (fieldParts.Length < 2)
                {
                    throw new ArgumentException($"Field '{field}' is not correctly formatted. It should contain at least a name and a type.");
                }

                var fieldName = fieldParts[0];
                var fieldTypeWithLength = fieldParts[1];
                string length = string.Empty;

                // Check if fieldType contains a length specification (e.g., varchar(5), decimal(16,2))
                if (fieldTypeWithLength.Contains("(") && fieldTypeWithLength.Contains(")"))
                {
                    var startIndex = fieldTypeWithLength.IndexOf('(') + 1;
                    var endIndex = fieldTypeWithLength.IndexOf(')');
                    length = fieldTypeWithLength.Substring(startIndex, endIndex - startIndex);
                    fieldTypeWithLength = fieldTypeWithLength.Substring(0, startIndex - 1); // Get the type without the length part
                }

                string validationAttributes = string.Empty;

                if ((fieldTypeWithLength.ToLower() == "varchar" && !string.IsNullOrEmpty(length)) || (fieldTypeWithLength.ToLower() == "char" && !string.IsNullOrEmpty(length)))
                {
                    if (int.TryParse(length, out int parsedLength))
                    {
                        validationAttributes = $@"
        [Required(ErrorMessage = ""El campo {fieldName} es obligatorio"")]
        [MaxLength({parsedLength}, ErrorMessage = ""El campo {fieldName} no puede tener más de {parsedLength} caracteres"")]
        [MinLength({parsedLength}, ErrorMessage = ""El campo {fieldName} debe tener al menos {parsedLength} caracteres"")]";
                    }
                    else
                    {
                        throw new ArgumentException($"Invalid length for varchar field '{fieldName}'. Length must be an integer.");
                    }
                }
                else if (fieldTypeWithLength.ToLower() == "decimal" && !string.IsNullOrEmpty(length))
                {
                    // For decimal, handle the precision and scale part, e.g., decimal(16,2)
                    var parts = length.Split(',');
                    if (parts.Length == 2 && int.TryParse(parts[0], out int precision) && int.TryParse(parts[1], out int scale))
                    {
                        validationAttributes = $@"
        [Range(0, {Math.Pow(10, precision) - 1}, ErrorMessage = ""El campo {fieldName} debe estar entre 0 y {Math.Pow(10, precision) - 1}"")]";
                    }
                    else
                    {
                        throw new ArgumentException($"Invalid length for decimal field '{fieldName}'. Length should be in the format decimal(precision, scale).");
                    }
                }

                modelProperties.Add($@"
            {validationAttributes}
        public {ConvertSqlTypeToCSharpType(fieldTypeWithLength)} {fieldName} {{ get; set; }}");
            }

            return string.Join(Environment.NewLine, modelProperties);
        }


        private string ConvertSqlTypeToCSharpType(string sqlType)
        {
            return sqlType.ToLower() switch
            {
                "varchar" => "string",
                "char" => "string",
                "money" => "decimal",
                "decimal" => "decimal",
                "int" => "int",
                "datetime" => "DateTime",
                _ => "string",
            };
        }
    }
}
