// custom.js
$(document).ready(function () {
    $("#themVaoGioHang").click(function () {
        var maMon = $(this).data("mamon");
        var tenMon = $(this).data("tenmon");
        var hinhAnh = $(this).data("hinhanh");
        var soLuongMM = $("#soLuongMM").val();

        $.ajax({
            url: "/GioHangController/ThemVaoGioHang?maMon=" + maMon + "&soLuongMM=" + soLuongMM + "&tenMon=" + tenMon + "&hinhAnh=" + hinhAnh,
            type: "POST",
            success: function (result) {
                console.log(result);
            }
        });
    });
});

document.addEventListener("DOMContentLoaded", function () {
    var form = document.querySelector("form");
    var cartTotal = document.getElementById("cart_total");

    form.addEventListener("submit", function (event) {
        event.preventDefault();
        var soLuong = parseInt(form.querySelector("input[name='soLuongMM']").value);
        var currentTotal = parseInt(cartTotal.textContent);
        var newTotal = currentTotal + soLuong;
        cartTotal.textContent = newTotal;
    });
});
