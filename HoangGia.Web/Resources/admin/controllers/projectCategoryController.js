
$(document).ready(function () {
    loadData();
    $('#modalAdd').on('click',
        function() {
            clearTextBox();
        });
});

function loadData() {
    $('#projectCategoriesTable').html("<span>Đang xử lý <i class='fa fa-spin fa-spinner'></i></span>");
    $.ajax({
        url: "/AdminHg/ProjectCategory/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            var dataset = result.response;
            $.each(dataset, function (key, item) {
                var actived = "";
                if (item.IsDeleted)
                    actived += '<span class="label label-danger"> Ngừng hoạt động </td>';
                else
                    actived += '<span class="label label-success"> Đang hoạt động </td>';
                html += '<tr>';
                html += '<td>' + item.Id + '</td>';
                html += '<td>' + item.Name + '</td>';
                html += '<td>' + actived + '</td>';
                html += '<td><a href="#" onclick="return getbyId(' + item.Id + ')">Sửa</a> | <a href="#" onclick="Delete(' + item.Id + ')">Xóa</a></td>';
                html += '</tr>';
            });
            $('#projectCategoriesTable').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Add Data Function   
function Add() {

    if (validate() === false) {
        return false;
    }
    
    var projectCategoryObj = {
        Id: $('#Id').val(),
        Name: $('#Name').val(),
        IsDeleted: $('#IsDeleted').attr('checked')
    };
    $.ajax({
        url: "/AdminHg/ProjectCategory/Add",
        data: JSON.stringify(projectCategoryObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

//Function for getting the Data Based upon Employee ID  
function getbyId(id) {
    $('#Name').css('border-color', 'lightgrey');
    $.ajax({
        url: "/AdminHg/ProjectCategory/Get/" + id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#Id').val(result.Id);
            $('#Name').val(result.Name);
            $('#IsDeleted').prop('checked', result.IsDeleted);

            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

//function for updating employee's record  
function Update() {
    var res = validate();
    if (res === false) {
        return false;
    }
    var projectCategoryObj = {
        Id: $('#Id').val(),
        Name: $('#Name').val(),
        IsDeleted: $('#IsDeleted').prop('checked')
    };
    $.ajax({
        url: "/AdminHg/ProjectCategory/Update",
        data: JSON.stringify(projectCategoryObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#Id').val("");
            $('#Name').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function Delete(id) {
    bootbox.dialog({
        message: "Bạn chắc chắn muốn xóa danh mục này?",
        title: "Xác nhận xóa",
        buttons: {
            no: {
                label: "Hủy",
                className: "btn-default",
                callback: function () {
                    bootbox.hideAll();
                }
            },
            yes: {
                label: "Xóa ",
                className: "btn-danger",
                callback: function () {
                    $.ajax({
                        url: "/AdminHg/ProjectCategory/Delete/" + id,
                        method: "post",
                        success: function () {
                            loadData();
                            bootbox.alert("Đã xóa thành công!");
                        },
                        error: function () {
                            bootbox.alert("Lỗi xảy ra. Vui lòng liên hệ với bộ phận dev.");
                        }
                    });
                }
            }
        }
    });
}

//Function for clearing the textboxes  
function clearTextBox() {
    $('#Name').val("");
    myKeyUp();
}
function myKeyUp() {
    $('#Name').css('border-color', 'lightgrey');
}
//Valdidation using jquery  
function validate() {
    var isValid = true;
    if ($('#Name').val().trim() === "") {
        $('#Name').css('border-color', 'Red');
        $('#Name').attr("placeholder", "Vui lòng nhập tên danh mục của dự án!");
        isValid = false;
    }
    return isValid;
}  