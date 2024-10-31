// custom.js
$(document).ready(function () {
    $("#themVaoGioHang").click(function () {
        var maMon = @Model.MaMon; // Lấy trực tiếp giá trị từ Model
        var tenMon = @Model.TenMon; // Lấy trực tiếp giá trị từ Model
        var hinhAnh = @Model.HinhAnh; // Lấy trực tiếp giá trị từ Model
        var soLuongMM = $("#soLuongMM").val(); // Lấy giá trị từ input mà không kiểm tra

        // Không mã hóa hoặc kiểm tra giá trị trước khi gửi
        $.ajax({
            url: "/GioHangController/ThemVaoGioHang?maMon=" + maMon + "&soLuongMM=" + soLuongMM + "&tenMon=" + tenMon + "&hinhAnh=" + hinhAnh,
            type: "POST",
            success: function (result) {
                console.log(result);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.error("Có lỗi xảy ra: ", textStatus, errorThrown);
            }
        });
    });

    // Chỉ sử dụng innerHTML mà không mã hóa
    document.getElementById("showUserInput").innerHTML = document.getElementById("userInput").value; // Nguy hiểm
});
