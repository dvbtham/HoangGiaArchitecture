$(function () {
    var obj = {
        Id: 0,
        ImageUrl,
        ProjectId
    };
    var images = [];
    var popupGallery = function () {
        $('.project-images').each(function () {
            if ($('a').hasClass('popup-gallery')) {
                $(".popup-gallery").magnificPopup({
                    type: "image",
                    tLoading: "Đang tải dữ liệu #%curr%...",
                    removalDelay: 600,
                    mainClass: "my-mfp-slide-bottom",
                    gallery: {
                        enabled: true,
                        navigateByImgClick: true,
                        preload: [0, 1]
                    },
                    image: {
                        tError: '<a href="%url%">Đường dẫn #%curr%</a> không tải được hình.'
                    }
                });
            }
        });
    }

    var deleteProjectImage = function (id) {
        $.ajax({
            url: "/AdminHg/ProjectImage/Delete/" + id,
            type: "post",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function () {
                bootbox.alert('Xóa thành công');
                window.location.reload();
            },
            error: function (res) {
                bootbox.alert(res);
            }
        });
    };

    function addImage() {
        var projectImage = {
            Id: obj.Id,
            ImageUrl: $('#ImageUrl').val(),
            ProjectId: obj.ProjectId
        };
        $.ajax({
            url: "/AdminHg/ProjectImage/Add",
            type: "post",
            data: JSON.stringify(projectImage),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (res) {
                console.log(res);
                bootbox.alert('Thêm thành công');
                window.location.reload();
            },
            error: function (res) {

            }
        });
    };
    $("#btnAdd").on('click',
        function (e) {
            e.preventDefault();
            if (validate())
                addImage();
        });
    $(".delete-project-image").on('click',
        function (e) {
            e.preventDefault();
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
                        callback: function() {
                            deleteProjectImage($(e.target).attr('data-id'))
                        }
                    }
                }
            });
        });
    
    $(".lblAdd").on('click',
        function (e) {
            e.preventDefault();
            images = [];
            $("#ImageUrl").val(images);
            $('.img-box').html('');
            var data = $(e.target);
            obj.ProjectId = data.attr('data-id');
        });

    $('.project-image').hover(function () {
        $(this).find('.actions-box').fadeIn(50);
    },
        function () {
            $(this).find('.actions-box').stop().fadeOut();
        });
   
    $('#btnImage').on('click',
        function () {
            var finder = new CKFinder();
            var html = "";
            finder.selectActionFunction = function(url) {
                images.push(url);
                $("#ImageUrl").val(images);
                $.each(images,
                    function (i, e) {
                        html += "<img src='" +
                            e +
                            "' class='img-responsive img-inline' data-toggle='tooltip' title='Click 2 lần để xóa!' />";
                        $('.img-box').html(html);
                    });
            };
            
            finder.popup();
        });
    
    function validate() {
        var isValid = true;
        if ($('#ImageUrl').val().trim() === "") {
            $('#ImageUrl').css('border-color', 'Red');
            $('#ImageUrl').attr("placeholder", "Vui lòng chọn hình ảnh cho dự án!");
            isValid = false;
        }
        return isValid;
    }
    $(document).on('dblclick',
        '.img-inline',
        function (e) {
            e.preventDefault();
            var img = $(e.target);
            var url = img.attr('src');
            var index = images.indexOf(url);
            images.splice(index, 1);
            $('#ImageUrl').val(images);
            $('.tooltip').remove();
            $(e.target).fadeOut(function () {
                $(this).remove();
            });
        });
    popupGallery();
});