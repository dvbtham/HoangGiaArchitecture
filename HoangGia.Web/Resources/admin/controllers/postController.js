$(function () {
    CKEDITOR.replace('content');
    $('#btnImage').on('click',
        function() {
            var finder = new CKFinder();
            finder.selectActionFunction = function(url) {
                $('#txtImageUrl').val(url);
            };
            finder.popup();
        });
});