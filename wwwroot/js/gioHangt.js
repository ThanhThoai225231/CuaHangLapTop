﻿// custom.js
$(document).ready(function () {
    $("#themVaoGioHang").click(function () {
        var maMon = "@Model.MaMon"; // Đặt trong dấu ngoặc kép nếu là chuỗi
        var tenMon = "@Model.TenMon"; // Đặt trong dấu ngoặc kép nếu là chuỗi
        var hinhAnh = "@Model.HinhAnh"; // Đặt trong dấu ngoặc kép nếu là chuỗi
        var soLuongMM = $("#soLuongMM").val();

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
            },
            error: function (xhr, status, error) {
                console.error("Có lỗi xảy ra:", error);
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
