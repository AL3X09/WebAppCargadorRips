//Archivo lista rips 
$(document).ready(function () {
    //$("#listaRips").DataTable();  
    //documentación de la biblioteca http://legacy.datatables.net/styling/custom_classes 

    $.fn.dataTable.ext.classes.sPageButtonActive = 'btn small blue lighten-2'; // Change Pagination Button Class
    $.fn.dataTable.ext.classes.sPageButtonActive = 'waves-light btn small'; // Change Pagination Button Class
    //$.fn.dataTable.ext.classes.sPageButtonStaticDisabled = 'btn small disabled'; // Change Pagination Button Class
   
    
    cargaratabla();
})

function cargaratabla() {
    
    $("#listaRips").DataTable(
        {
          "processing": true,
          "serverSide": true,
          "ajax": {
            "url": baseURL + "api/Rips/PostListadoRips",
			"method": "POST",
			"datatype": "json",
			"dataSrc": "Data"
			
          },
          "columns": [
            { "data": "CompanyName" }, 
			{ "data": "Address" }, 
			{ "data": "Postcode" },
            { "data": "Telephone" }
          ],
          "language": {
            "emptyTable": "There are no customers at present.",
            "zeroRecords": "There were no matching customers found.",
			"infoFiltered": " - filtered from _MAX_ records"
			
          },
          "searching": true,
          "ordering": true,
          "paging": true
         });  
    //
    $("select").val('10');
    //$('select').addClass("browser-default");
    $('select').material_select();
        
}

/*function startDataTable() {
// wait for scripts to load
if (!$.fn.DataTable) {
setTimeout(startDataTable, 100);
return;
}*/