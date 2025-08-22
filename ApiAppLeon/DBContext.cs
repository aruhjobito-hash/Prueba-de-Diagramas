using ApiAppLeon.Models;
using ApiAppLeon.Models.Configuracion;
using ApiAppLeon.Models.Contabilidad;
using ApiAppLeon.Models.Finanzas;
using ApiAppLeon.Models.KasNet;
using ApiAppLeon.Models.Logistica;
using ApiAppLeon.Models.Negocios;
using ApiAppLeon.Models.Operaciones;
using ApiAppLeon.Models.Planeamiento;
using ApiAppLeon.Models.Recursos_Humanos;
using ApiAppLeon.Models.Reportes_Anexos_SBS;
using ApiAppLeon.Models.Sistemas;
using ApiAppLeon.Models.Utilitarios;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace ApiAppLeon
{

    public partial class DBContext : DbContext
    {
        //public DBContext() { }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
        public DbSet<T> GetDynamicSet<T>() where T : class
        {
            return base.Set<T>();
        }
        public DbSet<T> GetDbSet<T>() where T : class
        {
            var property = this.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(p => p.PropertyType == typeof(DbSet<T>));

            if (property == null)
                throw new InvalidOperationException($"DbSet<{typeof(T).Name}> not found in context.");

            return (DbSet<T>)property.GetValue(this);
        }
        ////
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (optionsBuilder != null)
        //    {
        //        if (WebApplication.Create().Configuration.GetConnectionString("DESARROLLO") == "produccion")
        //            optionsBuilder.UseSqlServer(WebApplication.Create().Configuration.GetConnectionString("PRODUCCION"));
        //        else
        //            optionsBuilder.UseSqlServer(WebApplication.Create().Configuration.GetConnectionString("DESARROLLO"));
        //    }
        //}
        // Clase para obtener los datos del DbContext que las consultas o Stores traigan de la base de datos        
        public virtual DbSet<ConsultaDeudaRequest> ConsultaDeudaRequest { get; set; } = null!;
        public virtual DbSet<ConsultaDeudaResponse> ConsultaDeudaReponse { get; set; } = null!;
        public virtual DbSet<RegistraPagoRequest> RegistraPagoRequest { get; set; } = null!;
        public virtual DbSet<ExtornaPagoRequest> RegistraExtornoRequest { get; set; } = null!;
        public virtual DbSet<ConsultaPersonaRes> PersonaResponse { get; set; } = null!;
        public virtual DbSet<ConsultaPersonaResponse> ConsultaPersonaResponse { get; set; } = null!;
        public virtual DbSet<Agencias> ConsultaAgencia { get; set; } = null!;
        public virtual DbSet<ConsultaDeuda> ConsultaDeuda { get; set; } = null!;
        public virtual DbSet<ListarProductosBD> ListarProductosBDs { get; set; } = null!;
        public virtual DbSet<InteresComp> PaGetTasaIntBD { get; set; } = null!;
        public virtual DbSet<TransferenciaAhorroCreditoBD> TransferenciaAhorrosCreditoBDs { get; set; } = null!;
        public virtual DbSet<paListarConveniosBD> PaListarConveniosBDs { get; set; } = null!;
        public virtual DbSet<TasaIntAhoBD> TasaIntAhoBD { get; set; } = null!;
        public virtual DbSet<jwtDBLogin> jwtRegLogin { get; set; } = null!;
        public virtual DbSet<MenuDBModel> MenuDB { get; set; } = null!;
        //#region Required
        public virtual DbSet<TipoCambioDBModel> TipoCambioDB { get; set; } = null!; //migzav 25/02/2025
        public virtual DbSet<ValorLavadoActivosDBModel> ValorLavadoActivosDB { get; set; } = null!; //migzav 27/02/2025
        public virtual DbSet<CuentasDBModel> CuentasDB { get; set; } = null!; //migzav 03/03/2025
        public virtual DbSet<ProductosDBModel> ProductosDB { get; set; }//migzav 02/04/2025
        public virtual DbSet<ProductosNegociosDBModel> ProductosNegociosDB { get; set; } = null!;
        public virtual DbSet<HorarioDBModel> HorarioDB { get; set; } = null!;
        public virtual DbSet<FeriadoDBModel> FeriadoDB { get; set; } = null!;
        public virtual DbSet<ListaDBModel> ListaDB { get; set; } = null!;
        public virtual DbSet<PlanCuentasDBModel> PlanCuentasDB { get; set; } = null!;
        public virtual DbSet<ListaDetalleModelCuali> ListaDetalleCualiDB { get; set; } = null!;
        public virtual DbSet<ListaDetalleModelCuanti> ListaDetalleCuantiDB { get; set; } = null!;
        public virtual DbSet<AreaDBModel> PlanCuentasAreaDB { get; set; } = null!;
        public virtual DbSet<OperacionDBModel> OperacionDB { get; set; } = null!;
        public virtual DbSet<TabTipOpeModel> TabTipOpeDB { get; set; } = null!;
        public virtual DbSet<DocumentosModel> DocumentosDB { get; set; } = null!;
        public virtual DbSet<TabTipCuentaModel> TabTipCuentaDB { get; set; } = null!;
        public virtual DbSet<AccionOpexTipCtaModel> AccionOpeDB { get; set; } = null!;
        public virtual DbSet<DocxOpeModel> DocxOpeDB { get; set; } = null!;
        public virtual DbSet<ListaDestinosDBModel> ListaDestinosDB { get; set; }
        public virtual DbSet<RegistrarDestinosRequest> RegistrarDestinosRequest { get; set; }
        public virtual DbSet<ResultadoIdDenoCre> ResultadoIdDenoCre { get; set; }
        public virtual DbSet<ArticulosDBModel> ArticulosDB { get; set; } = null!; //migzav 15/04/2025



        public virtual DbSet<AuditoriaDBModel> AuditoriaDB { get; set; } = null!;                                           // VicVil 19/05/2025
        public virtual DbSet<AnexoAuditxCodDBModel> AnexoAuditxCodDB { get; set; } = null!;                                 // VicVil 19/05/2025
        public virtual DbSet<CodAnexoDBModel> CodAnexoDB { get; set; } = null!;                                             // VicVil 19/05/2025
        public virtual DbSet<requestCodSucaveDBModel> requestCodSucaveDB { get; set; } = null!;                             // VicVil 05/06/2025
        public virtual DbSet<SucaveDBModel> SucaveDB { get; set; } = null!;                                                 // VicVil 05/06/2025
        public virtual DbSet<SucaveStringDBModel> SucaveStringDB { get; set; } = null!;                                     // VicVil 14/07/2025   
        public virtual DbSet<Anexo13DBModel> Anexo13DB { get; set; } = null!;                                               // VicVil 26/04/2025
        public virtual DbSet<Anexo13SucaveDBModel> Anexo13SucaveDB { get; set; } = null!;                                   // VicVil 29/04/2025
        public virtual DbSet<BalanceComprobacionDBModel> BalanceComprobacionDB { get; set; } = null!;                       // VicVil 21/05/2025
        
        public virtual DbSet<Anexo5DeudoresDBModel> Anexo5DeudoresDB { get; set; } = null!;
        public virtual DbSet<Anexo6DBModel> Anexo6DB { get; set; } = null!;                                                 // VicVil 07/05/2025
        public virtual DbSet<Anexo6SucaveDBModel> Anexo6SucaveDB { get; set; } = null!;                                     // VicVil 29/04/2025
        public virtual DbSet<Reporte3DBModel> Reporte3DB { get; set; } = null!;                                             // VicVil 03/06/2025
        public virtual DbSet<Reporte2ADBModel> Reporte2ADB { get; set; } = null!;                                           // VicVil 09/06/2025
        public virtual DbSet<Anexo5DBModel> Anexo5DB { get; set; } = null!;                                                 // VicVil 02/05/2025
        public virtual DbSet<Reporte1DBModel> Reporte1DB { get; set; } = null!;                                             // VicVil 10/06/2025
        public virtual DbSet<Reporte2DDBModel> Reporte2DDB { get; set; } = null!;                                           // VicVil 13/06/2025
        public virtual DbSet<Reporte13DBModel> Reporte13DB { get; set; } = null!;                                           // VicVil 12/06/2025
        public virtual DbSet<Reporte6ADBModel> Reporte6ADB { get; set; } = null!;                                           // VicVil 13/06/2025
        public virtual DbSet<Reporte6BDBModel> Reporte6BDB { get; set; } = null!;                                           // VicVil 14/06/2025
        public virtual DbSet<EEFFSITUACIONFINANCIERADBModel> EEFFSITUACIONFINANCIERADB { get; set; } = null!;               // VicVil 16/06/2025
        public virtual DbSet<EEFFRESULTADOEJERCICIODBModel> EEFFRESULTADOEJERCICIODB { get; set; } = null!;                 // VicVil 17/06/2025

        public virtual DbSet<PerfilDBModel> PerfilDB { get; set; } = null!;

        public virtual DbSet<UnidadMedidaModel> UnidadMedidaDB { get; set; }

        public virtual DbSet<GrupoModel> GrupoDB { get; set; }

        public virtual DbSet<PlanCuentasModel> PlanCuentasDBLogistica { get; set; }

        public virtual DbSet<PresupuestosInversionesDBModel> PresupuestosInversionesDB { get; set; }

        public virtual DbSet<ReporteEjecutadoInversionesDBModel> ReporteEjecutadoInversionesDB { get; set; } //migzav 
        

        public virtual DbSet<AgenciasModel> AgenciasDB { get; set; }

        public virtual DbSet<AreasModel> AreasDB { get; set; }

        public virtual DbSet<PresupuestosUpdateModel> PresupuestosUpdateDB { get; set; }

        public virtual DbSet<PresupuestosFechaCierreModel> PresupuestosFechaCierreDB { get; set; }


        

        public virtual DbSet<ReportePresupuestadoDBModel> ReportePresupuestadoPresupuestosDB { get; set; } = null!;

        public virtual DbSet<ReporteComparativoMensualPresupuestosDBModel> ReporteComparativoMensualPresupuestosDB { get; set; } = null!;

        public virtual DbSet<ReporteComparativoTrimestralPresupuestosDBModel> ReporteComparativoTrimestralPresupuestosDB { get; set; } = null!;

        public virtual DbSet<ReporteComparativoSemestralPresupuestosDBModel> ReporteComparativoSemestralPresupuestosDB { get; set; } = null!;

        public virtual DbSet<ReporteComparativoAcumulativoPresupuestosDBModel> ReporteComparativoAcumulativoPresupuestosDB { get; set; } = null!;

        public virtual DbSet<ReporteEjecutadoDBModel> ReporteEjecutadoPresupuestosDB { get; set; } = null!;

        public virtual DbSet<ReporteComparativoMensualInversionesDBModel> ReporteComparativoMensualInversionesDB { get; set; } = null!;

        public virtual DbSet<ReporteComparativoTrimestralInversionesDBModel> ReporteComparativoTrimestralInversionesDB { get; set; } = null!;

        public virtual DbSet<ReporteComparativoSemestralInversionesDBModel> ReporteComparativoSemestralInversionesDB { get; set; } = null!;

        public virtual DbSet<ReporteAcumulativoInversionesDBModel> ReporteAcumulativoInversionesDB { get; set; } = null!;
        
        //public virtual DbSet<ReportePresupuestoAnualComparativoDBModel> ReportePresupuestoAnualComparativoDB { get; set; } = null!;



        public virtual DbSet<DatosTrabajadorDBModel> DatosTrabajadorDB { get; set; } = null!;
        public virtual DbSet<AgenciasDBModel> MantAgencias { get; set; } = null!;
        public virtual DbSet<AreasDBModel> MantAreasDB { get; set; } = null!;
        public virtual DbSet<CargosDBModel> CargosDB { get; set; } = null!;
        public virtual DbSet<EstructuraContableDBModel> EstructuraContableDB { get; set; } = null!;
        public virtual DbSet<CentroCostosDBModel> CentroCostosDB { get; set; } = null!;
        public virtual DbSet<PensionDBModel> PensionDB { get; set; } = null!;
        public virtual DbSet<SituacionEspDBModel> SituacionEspDB { get; set; } = null!;
        public virtual DbSet<PresupuestosIngresosEgresosDBModel> PresupuestosIngresosEgresosDB { get; set; } = null!;
        public virtual DbSet<TipContratoDBModel> TipContratoDB { get; set; } = null!;
        public virtual DbSet<TipTrabDBModel> TipTrabDB { get; set; } = null!;
        public virtual DbSet<TipMonedaDBModel> TipMonedaDB { get; set; } = null!;
        public virtual DbSet<TipCtaTraDBModel> TipCtaTraDB { get; set; } = null!;
        public virtual DbSet<EntBancDBModel> EntBancDB { get; set; } = null!;
        public virtual DbSet<NivelPersDBModel> NivelPersDB { get; set; } = null!;
        public virtual DbSet<CategPersDBModel> CategPersDB { get; set; } = null!;
        public virtual DbSet<AreasNDBModel> AreasNDB { get; set; } = null!;
        public virtual DbSet<CargosNDBModel> CargosNDB { get; set; } = null!;
        public virtual DbSet<RegistroMiembrosFamDBModel> RegistroMiembrosFamDB { get; set; } = null!;
        public virtual DbSet<PersonaFamModel> ReporteMiembrosFam { get; set; } = null!;
        public virtual DbSet<RegistroMiembrosFamTable> RegistroMiembrosFamTable { get; set; } = null;
        public virtual DbSet<RegistroTrabajadorDBModel> RegistroTrabajadorDB { get; set; } = null!;
        public virtual DbSet<ListarCombosRegistraPersonaBD> ListarCombosRegistraPersonaBDs { get; set; } = null;
        public virtual DbSet<PEIDBModel> PEIDB { get; set; } = null!;                                                           // VicVil PEI - 25/07/2025
        public virtual DbSet<RequestPEIListarDBModel> RequestPEIListarDB { get; set; } = null!;                                 // VicVil PEI - 26/07/2025
        public virtual DbSet<RESPONSABLESDBModel> RESPONSABLESDB { get; set; } = null!;                                         // VicVil PEI - 30/07/2025
        public virtual DbSet<PlanOperativoDBModel> PlanOperativoDB { get; set; } = null!;                                       // VicVil PEI - 30/07/2025
        public virtual DbSet<PerspectivaDBModel> PerspectivaDB { get; set; } = null!;                                           // VicVil PEI - 30/07/2025
        public virtual DbSet<PlanillaBoletaDBModel> PlanillaBoletaDB { get; set; } = null!;
        public virtual DbSet<ImprimeBoletaDBModel> PlanillaListarPlanilla { get; set; } = null!;
        public virtual DbSet<DetPlanillaBoletaDBModel> DetPlanillaBoletaDB { get; set; } = null!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
            modelBuilder.Entity<CargosNDBModel>().HasNoKey();
            modelBuilder.Entity<AreasNDBModel>().HasNoKey();
            modelBuilder.Entity<CategPersDBModel>().HasNoKey();
            modelBuilder.Entity<NivelPersDBModel>().HasNoKey();
            modelBuilder.Entity<EntBancDBModel>().HasNoKey();
            modelBuilder.Entity<TipCtaTraDBModel>().HasNoKey();
            modelBuilder.Entity<TipMonedaDBModel>().HasNoKey();
            modelBuilder.Entity<TipTrabDBModel>().HasNoKey();
            modelBuilder.Entity<TipContratoDBModel>().HasNoKey();
            modelBuilder.Entity<SituacionEspDBModel>().HasNoKey();
            modelBuilder.Entity<PensionDBModel>().HasNoKey();
            modelBuilder.Entity<CentroCostosDBModel>().HasNoKey();
            modelBuilder.Entity<EstructuraContableDBModel>().HasNoKey();
            modelBuilder.Entity<CargosDBModel>().HasNoKey();
            modelBuilder.Entity<DocxOpeModel>().HasNoKey();
            modelBuilder.Entity<AccionOpexTipCtaModel>().HasNoKey();
            modelBuilder.Entity<TabTipCuentaModel>().HasNoKey();
            modelBuilder.Entity<DocumentosModel>().HasNoKey();
            modelBuilder.Entity<TabTipOpeModel>().HasNoKey();
            modelBuilder.Entity<OperacionDBModel>().HasNoKey();
            modelBuilder.Entity<AreaDBModel>().HasNoKey();
            modelBuilder.Entity<PlanCuentasDBModel>().HasNoKey();
            modelBuilder.Entity<ListaDBModel>().HasNoKey();
            modelBuilder.Entity<ListaDetalleModelCuali>().HasNoKey();
            modelBuilder.Entity<ListaDetalleModelCuanti>().HasNoKey();
            modelBuilder.Entity<MenuDBModel>().HasNoKey();
            modelBuilder.Entity<jwtDBLogin>().HasNoKey();
            modelBuilder.Entity<TasaIntAhoBD>().HasNoKey();
            modelBuilder.Entity<paListarConveniosBD>().HasNoKey();
            modelBuilder.Entity<Agencias>().HasNoKey();
            //17/07/2024 JosAra Configuración con Keyless para entidades  
            modelBuilder.Entity<ConsultaPersonaRes>().HasNoKey();
            modelBuilder.Entity<ConsultaDeudaRequest>().HasNoKey();
            modelBuilder.Entity<TransferenciaAhorroCreditoBD>().HasNoKey();
            modelBuilder.Entity<lstdebt>().HasNoKey();
            modelBuilder.Entity<conceptosAdicionalesDeuda>().HasNoKey();
            modelBuilder.Entity<RegistraPagoRequest>().HasNoKey();
            modelBuilder.Entity<conceptosAdicionalesPago>().HasNoKey();
            modelBuilder.Entity<RegistraPagoResponse>().HasNoKey();
            modelBuilder.Entity<ExtornaPagoRequest>().HasNoKey();
            modelBuilder.Entity<conceptosAdicionalesExtorno>().HasNoKey();
            modelBuilder.Entity<ExtornaPagoResponse>().HasNoKey();
            modelBuilder.Entity<DBClassModel>().HasNoKey();
            modelBuilder.Entity<ConsultaPersonaRes>().HasNoKey();
            modelBuilder.Entity<ConsultaPersonaResponse>().HasNoKey();
            modelBuilder.Entity<ConsultaDeuda>().HasNoKey();
            modelBuilder.Entity<ListarProductosBD>().HasNoKey();
            modelBuilder.Entity<InteresComp>().HasNoKey();
            modelBuilder.Entity<TipoCambioDBModel>().HasNoKey(); //migzav 25/02/2025
            modelBuilder.Entity<ValorLavadoActivosDBModel>().HasNoKey(); //migzav 27/02/2025
            modelBuilder.Entity<CuentasDBModel>().HasNoKey(); //migzav 03/03/2025
            modelBuilder.Entity<ProductosDBModel>().HasNoKey(); //migzav 03/03/2025
            modelBuilder.Entity<ProductosNegociosDBModel>().HasNoKey();
            modelBuilder.Entity<HorarioDBModel>().HasNoKey();
            modelBuilder.Entity<FeriadoDBModel>().HasNoKey();
            modelBuilder.Entity<ListaDestinosDBModel>().HasNoKey(); //migzav 11/04/2025
            modelBuilder.Entity<RegistrarDestinosRequest>().HasNoKey(); //migzav 11/04/2025
            modelBuilder.Entity<ResultadoIdDenoCre>().HasNoKey(); //migzav 11/04/2025
            modelBuilder.Entity<PerfilDBModel>().HasNoKey();
            modelBuilder.Entity<ArticulosDBModel>().HasNoKey(); //migzav 15/04/2025
            modelBuilder.Entity<PresupuestosInversionesDBModel>().HasNoKey();
            modelBuilder.Entity<DatosTrabajadorDBModel>().HasNoKey();
            modelBuilder.Entity<AgenciasDBModel>().HasNoKey();
            modelBuilder.Entity<AreasDBModel>().HasNoKey();
            modelBuilder.Entity<ResultadoIdDenoCre>().HasNoKey(); //migzav 11/04/2025            
            modelBuilder.Entity<PresupuestosIngresosEgresosDBModel>().HasNoKey();


            modelBuilder.Entity<ReporteComparativoMensualPresupuestosDBModel>().HasNoKey();
            modelBuilder.Entity<AuditoriaDBModel>().HasNoKey();                     // VicVil 19/05/2025
            modelBuilder.Entity<AnexoAuditxCodDBModel>().HasNoKey();                // VicVil 19/05/2025
            modelBuilder.Entity<CodAnexoDBModel>().HasNoKey();                      // VicVil 19/05/2025
            modelBuilder.Entity<requestCodSucaveDBModel>().HasNoKey();              // VicVil 19/05/2025
            modelBuilder.Entity<SucaveDBModel>().HasNoKey();                        // VicVil 19/05/2025
            modelBuilder.Entity<Reporte3DBModel>().HasNoKey();                      // VicVil 03/06/2025
            modelBuilder.Entity<Anexo13DBModel>().HasNoKey();                       // VicVil 26/04/2025
            modelBuilder.Entity<Anexo13SucaveDBModel>().HasNoKey();                 // VicVil 29/04/2025
            modelBuilder.Entity<Anexo6DBModel>().HasNoKey();                        // VicVil 06/05/2025
            modelBuilder.Entity<Anexo6SucaveDBModel>().HasNoKey();                  // VicVil 29/04/2025
            modelBuilder.Entity<BalanceComprobacionDBModel>().HasNoKey();           // VicVil 21/05/2025       
            modelBuilder.Entity<Anexo5DBModel>().HasNoKey();                        // VicVil 02/05/2025
            modelBuilder.Entity<Reporte2ADBModel>().HasNoKey();                     // VicVil 09/06/2025
            modelBuilder.Entity<Reporte1DBModel>().HasNoKey();                      // VicVil 10/06/2025

            modelBuilder.Entity<RegistroMiembrosFamDBModel>().HasNoKey();           // JosAra 07/07/2025
            modelBuilder.Entity<RegistroTrabajadorDBModel>().HasNoKey();
            modelBuilder.Entity<ListarCombosRegistraPersonaBD>().HasNoKey();
            modelBuilder.Entity<PersonaFamModel>().HasNoKey();
            modelBuilder.Entity<RegistroMiembrosFamTable>()
           .HasKey(e => new { e.IdPersonaTrab, e.IdPersonaFam });
            modelBuilder.Entity<AuditoriaDBModel>().HasNoKey();                                 // VicVil 19/05/2025
            modelBuilder.Entity<AnexoAuditxCodDBModel>().HasNoKey();                            // VicVil 19/05/2025
            modelBuilder.Entity<CodAnexoDBModel>().HasNoKey();                                  // VicVil 19/05/2025
            modelBuilder.Entity<requestCodSucaveDBModel>().HasNoKey();                          // VicVil 19/05/2025
            modelBuilder.Entity<SucaveDBModel>().HasNoKey();                                    // VicVil 19/05/2025
            modelBuilder.Entity<SucaveStringDBModel>().HasNoKey();
            modelBuilder.Entity<Reporte3DBModel>().HasNoKey();                                  // VicVil 03/06/2025
            modelBuilder.Entity<Anexo13DBModel>().HasNoKey();                                   // VicVil 26/04/2025
            modelBuilder.Entity<Anexo13SucaveDBModel>().HasNoKey();                             // VicVil 29/04/2025
            modelBuilder.Entity<Anexo6DBModel>().HasNoKey();                                    // VicVil 06/05/2025
            modelBuilder.Entity<Anexo6SucaveDBModel>().HasNoKey();                              // VicVil 29/04/2025
            modelBuilder.Entity<BalanceComprobacionDBModel>().HasNoKey();                       // VicVil 21/05/2025       
            modelBuilder.Entity<Anexo5DBModel>().HasNoKey();                                    // VicVil 02/05/2025
            modelBuilder.Entity<Reporte2ADBModel>().HasNoKey();                                 // VicVil 09/06/2025
            modelBuilder.Entity<Reporte1DBModel>().HasNoKey();                                  // VicVil 10/06/2025
            modelBuilder.Entity<Reporte13DBModel>().HasNoKey();                                 // VicVil 12/06/2025
            modelBuilder.Entity<Reporte2DDBModel>().HasNoKey();                                 // VicVil 13/06/2025
            modelBuilder.Entity<Reporte6ADBModel>().HasNoKey();                                 // VicVil 13/06/2025
            modelBuilder.Entity<Reporte6BDBModel>().HasNoKey();                                 // VicVil 14/06/2025
            modelBuilder.Entity<EEFFSITUACIONFINANCIERADBModel>().HasNoKey();                   // VicVil 16/06/2025
            modelBuilder.Entity<EEFFRESULTADOEJERCICIODBModel>().HasNoKey();                    // VicVil 17/06/2025
            modelBuilder.Entity<Anexo5DeudoresDBModel>().HasNoKey();
            modelBuilder.Entity<PEIDBModel>().HasNoKey();                                       // VicVil 25/07/2025
            modelBuilder.Entity<RequestPEIListarDBModel>().HasNoKey();                          // VicVil 25/07/2025
            modelBuilder.Entity<RESPONSABLESDBModel>().HasNoKey();                              // VicVil 30/07/2025
            modelBuilder.Entity<PlanOperativoDBModel>().HasNoKey();                             // VicVil 30/07/2025
            modelBuilder.Entity<PerspectivaDBModel>().HasNoKey();                               // VicVil 30/07/2025
            modelBuilder.Entity<Anexo5DeudoresDBModel>().HasNoKey();
            modelBuilder.Entity<PlanillaBoletaDBModel>().HasNoKey();
            modelBuilder.Entity<ImprimeBoletaDBModel>().HasNoKey();
            modelBuilder.Entity<DetPlanillaBoletaDBModel>().HasNoKey();
        }
        //#endregion
    }
            

        
    //#endregion
}






