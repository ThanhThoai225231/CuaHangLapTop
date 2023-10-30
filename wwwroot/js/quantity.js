function incrementQuantity() {
    var quantityInput = document.getElementById('quantity_651c4009d6d2e');
    var currentValue = parseInt(quantityInput.value, 10);
    var maxValue = parseInt(quantityInput.getAttribute('max'), 10);

    if (!isNaN(currentValue) && currentValue < maxValue) {
        quantityInput.value = currentValue + 1;
    }
}

function decrementQuantity() {
    var quantityInput = document.getElementById('quantity_651c4009d6d2e');
    var currentValue = parseInt(quantityInput.value, 10);
    var minValue = parseInt(quantityInput.getAttribute('min'), 10);

    if (!isNaN(currentValue) && currentValue > minValue) {
        quantityInput.value = currentValue - 1;
    }
}

function validateInput(input) {
    var value = parseInt(input.value, 10);
    var minValue = parseInt(input.getAttribute('min'), 10);
    var maxValue = parseInt(input.getAttribute('max'), 10);

    if (isNaN(value) || value < minValue) {
        input.value = minValue;
    } else if (value > maxValue) {
        input.value = maxValue;
    }
}
