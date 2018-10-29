using System.Web;
using System.Web.Optimization;

namespace WebAppCargadorRips
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/validator.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/iziToast.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/style.css",
                      "~/Content/font-awesome.css",
                      "~/Content/iziToast.min.css"
                      ));

            // Estilos desing para boostrap solo la uso en el inici de sesion y registro
            bundles.Add(new StyleBundle("~/Content/mdb").Include(
                      "~/Content/mdb.css",
                      "~/Content/font-awesome.css",
                      "~/Content/sweetalert2.css",
                      "~/Content/compiled.min.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/jsmdb").Include(
                      "~/Scripts/sweetalert2.js",
                      "~/Scripts/mdb.js"));

            // Estilos desing para material solo la uso en el resto de la aplicacion
            bundles.Add(new StyleBundle("~/Content/material").Include(
                      "~/Content/materialize.css",
                      //"~/Content/materialize.min.css",
                      "~/Content/font-awesome.css",
                      "~/Content/material-icons.css",
                      "~/Content/sweetalert2.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/jsmaterial").Include(
                      "~/Scripts/materialize.js",
                      "~/Scripts/sweetalert2.js"
                      ));

            // Estilos o acciones externas
            bundles.Add(new StyleBundle("~/Content/estilosexternos").Include(
                       "~/Content/freestyle.css"
                       ));

            bundles.Add(new StyleBundle("~/Content/csserror").Include(
                      "~/Content/stylerror.css",
                      "~/Content/font-awesome.min.css"
                      ));

            // Fin Estilos acciones externas

            // Scripts librerias externas

            //JSGRID

            bundles.Add(new StyleBundle("~/Content/jsgridcss").Include(
                      "~/Content/jsgrid.css",
                      "~/Content/jsgrid-theme.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/jsGrid").Include(
                      "~/Scripts/jsgrid/jsgrid.js",
                      "~/Scripts/jsgrid/src/i18n/es.js",
                      "~/Scripts/jsgrid/src/jsgrid.*"

                      ));

            // jQuery-File-Upload
            /*desactive sta linea por dejar de usar la libreria la cual no me sirve por ahora
             * bundles.Add(new StyleBundle("~/Content/jQueryFileUploadcss").Include(
                      "~/Content/jquery.fileupload.css",
                      "~/Content/jquery.fileupload-ui.css",
                      "~/Content/jquery.fileupload-ui-noscript.css",
                      "~/Content/jquery.fileupload-noscript.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/jsjQueryFileUpload").Include(
                      "~/Scripts/vendor/jquery.ui.widget.js",
                      "~/Scripts/jquery.iframe-transport.js",
                      "~/Scripts/jquery.fileupload.js",
                      "~/Scripts/jquery.fileupload-process.js",
                      "~/Scripts/jquery.fileupload-image.js",
                      "~/Scripts/jquery.fileupload-validate.js",
                      "~/Scripts/jquery.fileupload-ui.js",
                      "~/Scripts/mainfileupload.js"
                      
                      ));*/

            // jQuery-File-Upload
            //desactivada no permite realizar acciones para que el aplicativo se requiere
            /*bundles.Add(new StyleBundle("~/Content/uploadifycss").Include(
                      "~/Content/uploadify/uploadify.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/uploadify").Include(
                      "~/Scripts/uploadify/jquery.uploadify.js"
                      //"~/Scripts/uploadify/uploadify.swf"

                      ));*/

            // multifile-master

            /*bundles.Add(new StyleBundle("~/Content/multifilecss").Include(
                      "~/Content/uploadify.css"
                      ));*/
					  
			//js tablas plugin datatable https://github.com/offspringer/mvc.datatables/blob/master/Mvc.Datatables.Sample.Legacy
            bundles.Add(new ScriptBundle("~/bundles/datatablesjs").Include(
                      "~/Scripts/DataTables/jquery.dataTables.min.js",
                      "~/Scripts/DataTables/dataTables.materialize0.js"
                      ));
            //dataTables.bootstrap.css"
            bundles.Add(new StyleBundle("~/Content/datatablescss").Include(
                      "~/Content/DataTables/css/dataTables.material.min.css"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/multifilejs").Include(
                      "~/Scripts/jquery.MultiFile.js"
                      ));

            // Utilizo tingle.js
            bundles.Add(new StyleBundle("~/Content/tinglecss").Include(
                      "~/Content/tingle.css"
                      //"~/Content/jquery.timepicker.min.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/tinglejs").Include(
                        "~/Scripts/tingle.js"
                        //"~/Scripts/jquery.timepicker.min.js"
                        ));

            // Utilizo http://franverona.com/loadgo/#what
            bundles.Add(new ScriptBundle("~/bundles/loadgojs").Include(
                        "~/Scripts/loadgo.js"
                        ));


            //DROPFY https://github.com/JeremyFagis/dropify

            bundles.Add(new StyleBundle("~/Content/dropifycss").Include(
                      "~/Content/dropify.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/dropifyjs").Include(
                      "~/Scripts/dropify.min.js"
                      ));

            //CHARTJS

            bundles.Add(new ScriptBundle("~/bundles/chartjs").Include(
                       "~/Scripts/chart/Chart.bundle.min.js",
                       "~/Scripts/chart/Chart.min.js",
                       "~/Scripts/require.js",
                      "~/Scripts/chart/utils.js"

                      ));

            //BACKGOUNDSESION
            bundles.Add(new StyleBundle("~/Content/backgroundcss").Include(
                      "~/Content/background/presentational-only.css",
                      "~/Content/background/responsive-full-background-image.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/backgroundjs").Include(
                       "~/Scripts/background/presentational-only.js"
                      ));

            //FIN  Scripts librerias externas

            // Scripts paginas
            //PAGINA SESION
            bundles.Add(new ScriptBundle("~/bundles/loginval").Include(
                      "~/Scripts/login.js"));
            //PAGINA LAYOUT
            bundles.Add(new ScriptBundle("~/bundles/layoutjs").Include(
                      "~/Scripts/layout.js"));
            //PAGINA HOME
            bundles.Add(new ScriptBundle("~/bundles/homeval").Include(
                      "~/Scripts/home.js"
                      ));

            //PAGINA LISTADO RIPS
            bundles.Add(new ScriptBundle("~/bundles/listadoripsjs").Include(
                      "~/Scripts/listadorips.js"
                      ));

            //PAGINA ADMINISTRACION
            bundles.Add(new ScriptBundle("~/bundles/administracionjs").Include(
                      "~/Scripts/administracion.js"));
            //PAGINA ADM USUARIOS
            bundles.Add(new ScriptBundle("~/bundles/admusuariosjs").Include(
                      "~/Scripts/admusuarios.js"));

            //PAGINA PRUEBAS
            bundles.Add(new ScriptBundle("~/bundles/pruebasjs").Include(
                      "~/Scripts/pruebas.js"));

            //PAGINA ADM ROLES
            bundles.Add(new ScriptBundle("~/bundles/rolesjs").Include(
                      "~/Scripts/admroles.js"));

            //PAGINA ADM PLANTILLAS CORREO
            bundles.Add(new ScriptBundle("~/bundles/plantillacorreojs").Include(
                      "~/Scripts/admplantillascorreo.js"));

            //PAGINA ADM PRGUNTAS FRECUENTES
            bundles.Add(new ScriptBundle("~/bundles/preguntasjs").Include(
                      "~/Scripts/admpreguntas.js"));

            //PAGINA ADM PRESENTACIONES DE AYUDA
            bundles.Add(new ScriptBundle("~/bundles/presentacionesjs").Include(
                      "~/Scripts/admpresentaciones.js"));

            //PAGINA ADM AUDITORIAS
            bundles.Add(new ScriptBundle("~/bundles/admauditoriasjs").Include(
                      "~/Scripts/admauditoria.js"));


            //PAGINA perfil usuario
            bundles.Add(new ScriptBundle("~/bundles/perfilusuariojs").Include(
                      "~/Scripts/perfilusuario.js"));
            //PAGINA cambio contraseña
            bundles.Add(new ScriptBundle("~/bundles/cambiocontraseniajs").Include(
                      "~/Scripts/cambiocontrasenia.js"));
            //PAGINA ayuda
            bundles.Add(new ScriptBundle("~/bundles/ayudajs").Include(
                      "~/Scripts/ayuda.js"));

            //PAGINA indicadores
            bundles.Add(new ScriptBundle("~/bundles/indicadoresjs").Include(
                      "~/Scripts/indicadores.js"));

            // Validadores
            //PAGINA HOME
            bundles.Add(new ScriptBundle("~/bundles/validarformarchivo").Include(
                      "~/Scripts/validaciones/validaCargaArchivo.js"));

            //CDN FUENTES DE LETRA

            //FIN CDN FUENTES DE LETRA
        }
    }
}
