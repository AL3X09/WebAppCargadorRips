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

            bundles.Add(new ScriptBundle("~/bundles/jsbootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                       "~/Scripts/validaciones/validator.js",
                      "~/Scripts/IziToast/iziToast.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/style.css",
                      "~/Content/Fontscss/font-awesome.css",
                      "~/Content/IziToast/iziToast.min.css"));

            // Estilos desing para boostrap solo la uso en el inici de sesion y registro
            bundles.Add(new StyleBundle("~/Content/mdbcss").Include(
                      "~/Content/MDB/mdb.css",
                      "~/Content/Fontscss/font-awesome.css",
                      //"~/Content/SweetAlert2/sweetalert2.css",
                      "~/Content/MDB/compiled.min.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/jsmdb").Include(
                      //"~/Scripts/SweetAlert2/sweetalert2.js",
                      "~/Scripts/MDB/mdb.js"));


            // Estilos desing para material solo la uso en el resto de la aplicacion
            bundles.Add(new StyleBundle("~/Content/material").Include(
                      "~/Content/Materialize/materialize.css",
                      //"~/Content/materialize.min.css",
                      "~/Content/Fontscss/font-awesome.css",
                      "~/Content/Materialize/material-icons.css",
                      "~/Content/SweetAlert2/sweetalert2.css",
                      "~/Content/IziToast/iziToast.min.css",
                      "~/Content/Materialize/material-icons.css",
                      "~/Content/fonts/roboto/Roboto-Regular.woff2"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/jsmaterial").Include(
                      "~/Scripts/Materialize/materialize.js",
                      "~/Scripts/SweetAlert2/sweetalert2.js",
                      "~/Scripts/IziToast/iziToast.min.js"
                      ));


            // Estilos o acciones externas
            bundles.Add(new StyleBundle("~/Content/estilosexternos").Include(
                       "~/Content/freestyle.css"
                       ));

            bundles.Add(new StyleBundle("~/Content/csserror").Include(
                      "~/Content/stylerror.css",
                      "~/Content/Fontscss/font-awesome.min.css"
                      ));
            // Fin Estilos acciones externas

            //JSGRID

            bundles.Add(new StyleBundle("~/Content/jsgridcss").Include(
                      "~/Content/Jsgrid/jsgrid.css",
                      "~/Content/Jsgrid/jsgrid-theme.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/jsGrid").Include(
                      "~/Scripts/Jsgrid/jsgrid.js",
                      "~/Scripts/Jsgrid/src/i18n/es.js",
                      "~/Scripts/Jsgrid/src/jsgrid.*"
                      ));

            //DROPFY https://github.com/JeremyFagis/dropify

            bundles.Add(new StyleBundle("~/Content/dropifycss").Include(
                      "~/Content/Dropify/dropify.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/dropifyjs").Include(
                      "~/Scripts/Dropify/dropify.min.js"
                      ));

            // Utilizo tingle.js
            bundles.Add(new StyleBundle("~/Content/tinglecss").Include(
                      "~/Content/Tinglejs/tingle.css"
                      //"~/Content/jquery.timepicker.min.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/tinglejs").Include(
                        "~/Scripts/Tinglejs/tingle.js"
                        //"~/Scripts/jquery.timepicker.min.js"
                        ));

            // Utilizo http://franverona.com/loadgo/#what
            bundles.Add(new ScriptBundle("~/bundles/loadgojs").Include(
                        "~/Scripts/loadgo.js"
                        ));
            // Utilizo multifile
            bundles.Add(new ScriptBundle("~/bundles/multifilejs").Include(
                      "~/Scripts/jquery.MultiFile.js"
                      ));

            //CHARTJS
            bundles.Add(new ScriptBundle("~/bundles/chartjs").Include(
                       "~/Scripts/chart/Chart.bundle.min.js",
                       "~/Scripts/chart/Chart.min.js",
                       "~/Scripts/require.js",
                      "~/Scripts/chart/utils.js"

                      ));

            //js tablas plugin datatable https://github.com/offspringer/mvc.datatables/blob/master/Mvc.Datatables.Sample.Legacy
            bundles.Add(new ScriptBundle("~/bundles/datatablesjs").Include(
                      "~/Scripts/DataTables/jquery.dataTables.min.js",
                      "~/Scripts/DataTables/dataTables.materialize0.js"
                      ));
            //dataTables.bootstrap.css"
            bundles.Add(new StyleBundle("~/Content/datatablescss").Include(
                      "~/Content/DataTables/css/dataTables.material.min.css"
                     ));

            //Fuentes

            bundles.Add(new StyleBundle("~/Content/fuentescss").Include(
                      "~/Content/fonts/roboto/Roboto-Bold.woff",
                      "~/Content/fonts/roboto/Roboto-Light.woff",
                      "~/Content/fonts/roboto/Roboto-Medium.woff"
                      ));


            // Scripts paginas
            //PAGINA SESION
            bundles.Add(new ScriptBundle("~/bundles/loginval").Include(
                      "~/Scripts/Paginas/login.js"));
            //PAGINA LAYOUT
            bundles.Add(new ScriptBundle("~/bundles/layoutjs").Include(
                      "~/Scripts/Paginas/layout.js"));
            //PAGINA HOME
            bundles.Add(new ScriptBundle("~/bundles/homeval").Include(
                      "~/Scripts/Paginas/home.js"
                      ));
            //PAGINA LISTADO RIPS
            bundles.Add(new ScriptBundle("~/bundles/listadoripsjs").Include(
                      "~/Scripts/Paginas/listadorips.js"
                      ));
            //PAGINA perfil usuario
            bundles.Add(new ScriptBundle("~/bundles/perfilusuariojs").Include(
                      "~/Scripts/Paginas/perfilusuario.js"));
            //PAGINA cambio contraseña
            bundles.Add(new ScriptBundle("~/bundles/cambiocontraseniajs").Include(
                      "~/Scripts/Paginas/cambiocontrasenia.js"));
            //PAGINA indicadores
            bundles.Add(new ScriptBundle("~/bundles/indicadoresjs").Include(
                      "~/Scripts/paginas/indicadores.js"));
            //PAGINA ayuda
            bundles.Add(new ScriptBundle("~/bundles/ayudajs").Include(
                      "~/Scripts/paginas/ayuda.js"));
            //PAGINA ADMINISTRACION
            bundles.Add(new ScriptBundle("~/bundles/administracionjs").Include(
                      "~/Scripts/paginas/administracion.js"));


            // Validadores
            //PAGINA HOME
            bundles.Add(new ScriptBundle("~/bundles/validarformarchivo").Include(
                      "~/Scripts/validaciones/validaCargaArchivo.js"));
        }
    }
}
