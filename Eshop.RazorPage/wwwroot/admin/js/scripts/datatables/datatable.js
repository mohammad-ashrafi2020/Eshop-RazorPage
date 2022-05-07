/*=========================================================================================
    File Name: datatables-basic.js
    Description: Basic Datatable
==========================================================================================*/

$(document).ready(function () {

	$.extend( true, $.fn.dataTable.defaults, {
		language: {
			"sEmptyTable":     "هیچ داده ای در جدول وجود ندارد",
			"sInfo":           "نمایش _START_ تا _END_ از _TOTAL_ رکورد",
			"sInfoEmpty":      "نمایش 0 تا 0 از 0 رکورد",
			"sInfoFiltered":   "(فیلتر شده از _MAX_ رکورد)",
			"sInfoPostFix":    "",
			"sInfoThousands":  ",",
			"sLengthMenu":     "نمایش _MENU_ رکورد",
			"sLoadingRecords": "در حال بارگزاری...",
			"sProcessing":     "در حال پردازش...",
			"sSearch":         "جستجو: ",
			"sZeroRecords":    "رکوردی با این مشخصات پیدا نشد",
			"oPaginate": {
				"sFirst":    "ابتدا",
				"sLast":     "انتها",
				"sNext":     "بعدی",
				"sPrevious": "قبلی"
			},
			"oAria": {
				"sSortAscending":  ": فعال سازی نمایش به صورت صعودی",
				"sSortDescending": ": فعال سازی نمایش به صورت نزولی"
			}
		}
	} );

	/****************************************
	 *       js of zero configuration        *
	 ****************************************/

	$('.zero-configuration').DataTable();

	/********************************************
	 *        js of Order by the grouping        *
	 ********************************************/

	var groupingTable = $('.row-grouping').DataTable({
		"columnDefs": [{
			"visible": false,
			"targets": 2
		}],
		"order": [
			[2, 'asc']
		],
		"displayLength": 10,
		"autoWidth" : false,
		"drawCallback": function (settings) {
			var api = this.api();
			var rows = api.rows({
				page: 'current'
			}).nodes();
			var last = null;

			api.column(2, {
				page: 'current'
			}).data().each(function (group, i) {
				if (last !== group) {
					$(rows).eq(i).before(
						'<tr class="group"><td colspan="5">' + group + '</td></tr>'
					);

					last = group;
				}
			});
		}
	});

	$('.row-grouping tbody').on('click', 'tr.group', function () {
		var currentOrder = groupingTable.order()[0];
		if (currentOrder[0] === 2 && currentOrder[1] === 'asc') {
			groupingTable.order([2, 'desc']).draw();
		} else {
			groupingTable.order([2, 'asc']).draw();
		}
	});

	/*************************************
	 *       js of complex headers        *
	 *************************************/

	$('.complex-headers').DataTable({
		"autoWidth" : false
	});


	/*****************************
	 *       js of Add Row        *
	 ******************************/

	var t = $('.add-rows').DataTable();
	var counter = 2;

	$('#addRow').on('click', function () {
		t.row.add([
			counter + '.1',
			counter + '.2',
			counter + '.3',
			counter + '.4',
			counter + '.5'
		]).draw(false);

		counter++;
	});


	/**************************************************************
	 * js of Tab for COLUMN SELECTORS WITH EXPORT AND PRINT OPTIONS *
	 ***************************************************************/

	$('.dataex-html5-selectors').DataTable({
		dom: 'Bfrtip',
		buttons: [{
				text: 'کپی',
				extend: 'copyHtml5',
				exportOptions: {
					columns: [0, ':visible']
				}
			},
			{
				extend: 'pdfHtml5',
				exportOptions: {
					columns: ':visible'
				}
			},
			{
				text: 'JSON',
				action: function (e, dt, button, config) {
					var data = dt.buttons.exportData();

					$.fn.dataTable.fileSave(
						new Blob([JSON.stringify(data)]),
						'Export.json'
					);
				}
			},
			{
				text: 'چاپ',
				extend: 'print',
				exportOptions: {
					columns: ':visible'
				}
			}
		]
	});

	/**************************************************
	 *       js of scroll horizontal & vertical        *
	 **************************************************/

	$('.scroll-horizontal-vertical').DataTable({
		"scrollY": 200,
		"scrollX": true,
		"autoWidth" : false
	});
});