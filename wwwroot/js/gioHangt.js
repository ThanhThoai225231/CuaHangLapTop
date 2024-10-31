// custom.js
$(document).ready(function () {
    $("#themVaoGioHang").click(function () {
        // Lấy giá trị từ input mà không kiểm tra
        var maMon = $("#maMonInput").val(); // Người dùng có thể nhập mã độc
        var tenMon = $("#tenMonInput").val(); // Người dùng có thể nhập mã độc
        var hinhAnh = $("#hinhAnhInput").val(); // Người dùng có thể nhập mã độc
        var soLuongMM = $("#soLuongMM").val(); // Không kiểm tra kiểu dữ liệu

        // Gửi yêu cầu AJAX
        $.ajax({
            url: "/GioHangController/ThemVaoGioHang",
            type: "POST",
            data: {
                maMon: maMon,
                soLuongMM: soLuongMM,
                tenMon: tenMon,
                hinhAnh: hinhAnh
            },
            success: function (result) {
                console.log(result);

                // Hiển thị kết quả trên trang mà không mã hóa
                $("#resultContainer").html(result); // Nguy hiểm: có thể thực thi mã độc
            }
        });
    });

    // Chỉ sử dụng innerHTML mà không mã hóa
    document.getElementById("showUserInput").innerHTML = document.getElementById("userInput").value; // Nguy hiểm
});
