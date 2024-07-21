function sua(id){
    $("#Row0"+id).css("display", "none");
    $("#Row1" + id).css("display", "revert");
}
function huy(id) {
    $("#Row0" + id).css("display", "revert");
    $("#Row1" + id).css("display", "none");
}
function xoa(id) {
    if (confirm("Bạn muốn xóa sản phẩm?")) {
        $.ajax({
            type: "DELETE",
            url: "/Product/DeleteProduct",
            data: {
                idProduct: id
            },
            success: function (success) {
                alert(success);
                location.reload(true);
            },
            error: function (error) {
                alert("Lỗi");
            }
        });
    }
}
function huyThem() {
    $("#btnThem").css("display", "block");
    $("#btnFormThem").css("display", "none");
}
function them() {
    $("#btnThem").css("display", "none");
    $("#btnFormThem").css("display", "block");
}
setTimeout(function () {
    $("#msgAlert").fadeOut("slow");
}, 2000);
function DuyetHuyDon(id, trangthai) {
    var status = '';
    if (trangthai == "Đã duyệt") {
        status == "duyệt";
    }
    if (trangthai == "Chờ xử lý") {
        status = "hủy duyệt";
    }
    if (confirm("Bạn muốn " + status +" đơn hàng này?")) {
        $.ajax({
            type: "POST",
            url: "/Order/DuyetHuyDon",
            data: {
                id: id,
                trangthai: trangthai
            },
            success: function (success) {
                alert(success);
                location.reload(true);
            },
            error: function (error) {
                alert("Lỗi");
            }
        });
    }
}
function DetailOrder(id) {
    $.getJSON("/Order/GetOrderById?id=" + id, function (data) {
        var html = '';
        html += '<label>Tên khách hàng: ' + data[0].tenKH + '</label>'
        html += '<table class="table table-responsive table-bordered mt-2">'
        html += '<thead>'
        html += '<th>#</th>'
        html += '<th>Tên sản phẩm</th>'
        html += '<th>Đơn giá</th>'
        html += '<th>Số lượng</th>'
        html += '</thead>'

        html += '<tbody>'

        $.each(data, function (key, value) {
            html += '<tr>'
            html += '<th>'+(key+1)+'</th>'
            html += '<td><label>' + value.tenSP + '</label></td>'
            html += '<td><label style="width: 150px">' + numberWithCommas(value.donGia) + ' đồng</label></td>'
            html += '<td><label>' + value.soLuong + '</label></td>'
            html += '</tr>'
        });
        html += '</tbody>'
        html += '<table>'
        $(".modal-body").html(html);
        $("#exampleModal").modal("toggle");
    });
}
function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
}
$("#closeModal").on("click", function () {
    $('#exampleModal').modal('hide');
})