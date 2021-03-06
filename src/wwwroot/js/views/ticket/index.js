var popup, dataTable;
var entity = 'Ticket';
var apiurl = '/api/' + entity;
//Date: when = new Date(CreateAt);
const d = new Date("CreateAt");
let text = d.toString();

$(document).ready(function () {
    var organizationId = $('#organizationId').val();
    var DateCreated = $('#CreateAt').val();
    dataTable = $('#grid').DataTable({
        "ajax": {
            "url": apiurl + '/' + organizationId,
            "type": 'GET',
            "datatype": 'json'
        },
        "columns": [
            //{
            //    "data": "ticketId"
                
            //},
            {
                "data": "ticketNo"

            },
            //{
            //    "data": 3, "title": "Date Created",
            //    "render": function (data, type, full, meta) {
            //        return moment(data).format('YYYY-MM-DD');
            //    }
            //},
            {
                "data": "date"

            },
            //{
            //    targets: 3,
            //    render: $.fn.dataTable.render.moment('Do MMM YYYY')
            //},
            {
                "data": "ticketChannel"
            },
            {
                "data": "ticketStatus"
            },
            {
                "data": "rating"
            },
            {
                "data": "supportAgentId"
            },
            {
                "data": "ticketId",
                "render": function (data) {
                    var btnEdit = "<a class='btn btn-default btn-xs' onclick=ShowPopup('/" + entity + "/AddEdit/" + data + "')><i class='fa fa-pencil'></i></a>";
                    var btnDelete = "<a class='btn btn-danger btn-xs' style='margin-left:5px' onclick=Delete('" + data + "')><i class='fa fa-trash'></i></a>";
                    return btnEdit + btnDelete;
                }
            }
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "lengthChange": false,
    });
});

function ShowPopup(url) {
    var modalId = 'modalDefault';
    var modalPlaceholder = $('#' + modalId + ' .modal-dialog .modal-content');
    $.get(url)
        .done(function (response) {
            modalPlaceholder.html(response);
            popup = $('#' + modalId + '').modal({
                keyboard: false,
                backdrop: 'static'
            });
        });
}


function SubmitAddEdit(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        var data = $(form).serializeJSON();
        data = JSON.stringify(data);
        $.ajax({
            type: 'POST',
            url: apiurl,
            data: data,
            contentType: 'application/json',
            success: function (data) {
                if (data.success) {
                    popup.modal('hide');
                    ShowMessage(data.message);
                    dataTable.ajax.reload();
                } else {
                    ShowMessageError(data.message);
                }
            }
        });

    }
    return false;
}

function Delete(id) {
    swal({
        title: "Are you sure want to Delete?",
        text: "You will not be able to restore the record!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#dd4b39",
        confirmButtonText: "Yes, delete it!",
        closeOnConfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: apiurl + '/' + id,
            success: function (data) {
                if (data.success) {
                    ShowMessage(data.message);
                    dataTable.ajax.reload();
                } else {
                    ShowMessageError(data.message);
                }
            }
        });
    });


}




