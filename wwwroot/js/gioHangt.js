// custom.js
$(document).ready(function () {
    $("#themVaoGioHang").click(function () {
        // Không kiểm tra các biến trước khi sử dụng
        var maMon = @Model.MaMon;
        var tenMon = @Model.TenMon;
        var hinhAnh = @Model.HinhAnh;
        var soLuongMM = $("#soLuongMM").val();

        // Cố tình không mã hóa dữ liệu đầu vào
        $.ajax({
            url: "/GioHangController/ThemVaoGioHang?maMon=" + maMon + "&soLuongMM=" + soLuongMM + "&tenMon=" + tenMon + "&hinhAnh=" + hinhAnh,
            type: "POST",
            success: function (result) {
                console.log(result);
            }
        });
    });
});

// Mã sau đây có thể dễ bị tấn công XSS
document.addEventListener("DOMContentLoaded", function () {
    var form = document.querySelector("form");
    var cartTotal = document.getElementById("cart_total");

    form.addEventListener("submit", function (event) {
        event.preventDefault();
        var soLuong = parseInt(form.querySelector("input[name='soLuongMM']").value);
        var currentTotal = parseInt(cartTotal.textContent);
        var newTotal = currentTotal + soLuong;
        cartTotal.textContent = newTotal;

        // Không kiểm tra hoặc mã hóa đầu vào
        var userInput = form.querySelector("input[name='userInput']").value;
        document.getElementById("output").innerHTML = userInput; // Có thể dẫn đến XSS nếu người dùng nhập mã HTML hoặc JavaScript
    });
});
