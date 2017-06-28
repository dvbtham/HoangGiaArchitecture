$(document).ready(function() {
    $('#myTable').DataTable({
        'lengthMenu': [[10, 20, 25, 50, -1], [10, 20, 25, 50, 'All']],
        "paging": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": true,
        "language": {
            "lengthMenu": "Hiển thị _MENU_ kết quả trên 1 trang",
            "zeroRecords": "Không tìm thấy dữ liệu",
            "info": "Đang ở trang _PAGE_ trên tổng _PAGES_ trang",
            "infoEmpty": "Dữ liệu rỗng",
            "infoFiltered": "(filtered from _MAX_ total records)"
        }
    });
});