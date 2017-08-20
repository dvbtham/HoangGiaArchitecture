$(document).ready(function() {
    $("#saveWebInfo").on('click',
        function () {
            update();
        });
    $("#web-info-edit").on('click',
        function (e) {
            var button = $(e.target);
            var id = button.attr('data-id');
            edit(id);
        });
});

function update() {
    var setting = {
        Id: $("#Id").val(),
        Name: $("#Name").val(),
        PageTitle: $("#PageTitle").val(),
        Url: $("#Url").val(),
        WorkingDay: $("#WorkingDay").val(),
        CompanyAddress: $("#CompanyAddress").val(),
        Tel: $("#Tel").val(),
        Long: $("#Long").val(),
        Lat: $("#Lat").val(),
        ServiceDescription: $("#ServiceDescription").val(),
        ServiceScheme: $("#ServiceScheme").val(),
        WhyChooseUsTitles: $("#WhyChooseUsTitles").val(),
        Smtp: $("#Smtp").val(),
        SmtpUsername: $("#SmtpUsername").val(),
        SmtpPassword: $("#SmtpPassword").val(),
        SmtpPort: $("#SmtpPort").val(),
        SmtpEnableSsl: $("#SmtpEnableSsl").prop('checked'),
        AdminEmailAddress: $("#AdminEmailAddress").val(),
        NotificationReplyEmail: $("#NotificationReplyEmail").val()
    };
    if (!validate()) return false;
    $.ajax({
        url: '/AdminHg/Setting/Update',
        type: 'post',
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(setting),
        success: function() {
            alert('Bạn đã cập nhật thành công.');
            window.location.reload();
        },
        error: function() {
            alert('Lỗi đã xảy ra trong quá trình cập nhật, Vui lòng kiểm tra lại!');
        }
    });
}

function edit(id) {
    rightState();
    $.ajax({
        url: "/AdminHg/Setting/Edit/" + id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#Id').val(result.Id);
            $('#Name').val(result.Name);
            $('#PageTitle').val(result.PageTitle);
            $('#Url').val(result.Url);
            $('#Tel').val(result.Tel);
            $('#WorkingDay').val(result.WorkingDay);
            $('#CompanyAddress').val(result.CompanyAddress);
            $('#Long').val(result.Long);
            $('#Lat').val(result.Lat);
            $('#ServiceDescription').val(result.ServiceDescription);
            $('#ServiceScheme').val(result.ServiceScheme);
            $('#WhyChooseUsTitles').val(result.WhyChooseUsTitles);
            $('#Smtp').val(result.Smtp);
            $('#SmtpPort').val(result.SmtpPort);
            $('#SmtpUsername').val(result.SmtpUsername);
            $('#SmtpPassword').val(result.SmtpPassword);
            $('#AdminEmailAddress').val(result.AdminEmailAddress);
            $('#NotificationReplyEmail').val(result.NotificationReplyEmail);
            $('#SmtpEnableSsl').prop('checked', result.SmtpEnableSsl);
            $("#myModal").modal("show");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function validate() {
    var isValid = true;
    if ($('#Name').val().trim() === "") {
        $('#Name').css('border-color', 'Red');
        $('#Name').attr("placeholder", "Vui lòng nhập tên trang web!");
        isValid = false;
    }
    if ($('#PageTitle').val().trim() === "") {
        $('#PageTitle').css('border-color', 'Red');
        $('#PageTitle').attr("placeholder", "Vui lòng nhập tiêu đề trang web!");
        isValid = false;
    }
    if ($('#Url').val().trim() === "") {
        $('#Url').css('border-color', 'Red');
        $('#Url').attr("placeholder", "Vui lòng nhập địa chỉ trang web!");
        isValid = false;
    }
    if ($('#Tel').val().trim() === "") {
        $('#Tel').css('border-color', 'Red');
        $('#Tel').attr("placeholder", "Vui lòng nhập số điện thoại!");
        isValid = false;
    }
    if ($('#WorkingDay').val().trim() === "") {
        $('#WorkingDay').css('border-color', 'Red');
        $('#WorkingDay').attr("placeholder", "Vui lòng nhập ngày làm việc!");
        isValid = false;
    }
    if ($('#CompanyAddress').val().trim() === "") {
        $('#CompanyAddress').css('border-color', 'Red');
        $('#CompanyAddress').attr("placeholder", "Vui lòng nhập địa chỉ công ty!");
        isValid = false;
    }
    if ($('#ServiceDescription').val().trim() === "") {
        $('#ServiceDescription').css('border-color', 'Red');
        $('#ServiceDescription').attr("placeholder", "Vui lòng nhập ở đây!");
        isValid = false;
    }
    if ($('#ServiceScheme').val().trim() === "") {
        $('#ServiceScheme').css('border-color', 'Red');
        $('#ServiceScheme').attr("placeholder", "Vui lòng nhập ở đây!");
        isValid = false;
    }
    if ($('#WhyChooseUsTitles').val().trim() === "") {
        $('#WhyChooseUsTitles').css('border-color', 'Red');
        $('#WhyChooseUsTitles').attr("placeholder", "Vui lòng nhập ở đây!");
        isValid = false;
    }
    if ($('#Smtp').val().trim() === "") {
        $('#Smtp').css('border-color', 'Red');
        $('#Smtp').attr("placeholder", "Vui lòng nhập ở đây!");
        isValid = false;
    }
    if ($('#SmtpPort').val().trim() === "") {
        $('#SmtpPort').css('border-color', 'Red');
        $('#SmtpPort').attr("placeholder", "Vui lòng nhập ở đây!");
        isValid = false;
    }
    if ($('#SmtpUsername').val().trim() === "") {
        $('#SmtpUsername').css('border-color', 'Red');
        $('#SmtpUsername').attr("placeholder", "Vui lòng nhập ở đây!");
        isValid = false;
    }
    if ($('#SmtpPassword').val().trim() === "") {
        $('#SmtpPassword').css('border-color', 'Red');
        $('#SmtpPassword').attr("placeholder", "Vui lòng nhập ở đây!");
        isValid = false;
    }
    if ($('#AdminEmailAddress').val().trim() === "") {
        $('#AdminEmailAddress').css('border-color', 'Red');
        $('#AdminEmailAddress').attr("placeholder", "VVui lòng nhập ở đây!");
        isValid = false;
    }
    if ($('#NotificationReplyEmail').val().trim() === "") {
        $('#NotificationReplyEmail').css('border-color', 'Red');
        $('#NotificationReplyEmail').attr("placeholder", "Vui lòng nhập ở đây!");
        isValid = false;
    }
    if ($('#SmtpEnableSsl').val().trim() === "") {
        $('#SmtpEnableSsl').css('border-color', 'Red');
        $('#SmtpEnableSsl').attr("placeholder", "Vui lòng nhập ở đây!");
        isValid = false;
    }
    
    return isValid;
} 

function rightState() {
    $('#Name').bind('keyup',
        function() {
            $('#Name').css('border-color', '#d2d6de');

        });
    $('#PageTitle').bind('keyup',
        function () {
            $('#PageTitle').css('border-color', '#d2d6de');

        });
    $('#Url').bind('keyup',
        function () {
            $('#Url').css('border-color', '#d2d6de');

        });
    $('#Tel').bind('keyup',
        function () {
            $('#Tel').css('border-color', '#d2d6de');

        });
    $('#WorkingDay').bind('keyup',
        function () {
            $('#WorkingDay').css('border-color', '#d2d6de');

        });
    $('#CompanyAddress').bind('keyup',
        function () {
            $('#CompanyAddress').css('border-color', '#d2d6de');

        });
    $('#WhyChooseUsTitles').bind('keyup',
        function () {
            $('#WhyChooseUsTitles').css('border-color', '#d2d6de');

        });
    $('#ServiceDescription').bind('keyup',
        function () {
            $('#ServiceDescription').css('border-color', '#d2d6de');

        });
    $('#ServiceScheme').bind('keyup',
        function () {
            $('#ServiceScheme').css('border-color', '#d2d6de');

        });
    $('#Smtp').bind('keyup',
        function () {
            $('#Smtp').css('border-color', '#d2d6de');

        });
    $('#SmtpUsername').bind('keyup',
        function () {
            $('#SmtpUsername').css('border-color', '#d2d6de');

        });
    $('#SmtpPort').bind('keyup',
        function () {
            $('#SmtpPort').css('border-color', '#d2d6de');

        });
    $('#SmtpPassword').bind('keyup',
        function () {
            $('#SmtpPassword').css('border-color', '#d2d6de');

        });
    $('#SmtpEnableSsl').bind('keyup',
        function () {
            $('#SmtpEnableSsl').css('border-color', '#d2d6de');

        });
    $('#NotificationReplyEmail').bind('keyup',
        function () {
            $('#NotificationReplyEmail').css('border-color', '#d2d6de');

        });
    $('#AdminEmailAddress').bind('keyup',
        function () {
            $('#AdminEmailAddress').css('border-color', '#d2d6de');

        });
}