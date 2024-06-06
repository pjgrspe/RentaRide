function openModalAddOrder() {
    $('#addOrderModal').modal('show');
    // Calculate the date 18 years ago from today
    const today = new Date();

    // Initialize Flatpickr for the "Order Start Date" and "Order End Date" fields
    flatpickr(".flatpickr-orderstart", {
        dateFormat: "Y-m-dTH:i",
        enableTime: true,
        altInput: true,
        altFormat: "F j, Y, H:i",
        allowInput: true,
        defaultDate: "today",
    });

    flatpickr(".flatpickr-orderend", {
        dateFormat: "Y-m-dTH:i",
        enableTime: true,
        altInput: true,
        altFormat: "F j, Y, H:i",
        allowInput: true,
        defaultDate: "today",
    });
}

function closeModalAddOrder() {
    $('#addOrderModal').modal('hide');
}

/* ---------------------------------------------------
    USER TAB SCRIPTS
----------------------------------------------------- */
function openModalOrderDetails(orderId, receiptId, name, car, orderDate, returnDate, cost, extraFees, status, proofOfPaymentSrc) {
    $('#modalOrderId').text(orderId);
    $('#modalReceiptId').text(receiptId);
    $('#modalUserName').text(name);
    $('#modalCar').text(car);
    $('#modalOrderDate').text(orderDate);
    $('#modalReturnDate').text(returnDate);
    $('#modalCost').text(cost);
    $('#modalExtraFees').text(extraFees);
    $('#modalStatus').text(status);

    // Set the src attribute for the images
    $('#modalProofOfPayment img').attr('src', proofOfPaymentSrc);
    $('#proofOfPaymentImage').attr('src', proofOfPaymentSrc);
    //document.getElementById('orderID-Verify').value = orderId;

    $('#orderModal').modal('show');
}

function closeModalOrderDetails() {
    $('#modalOrderId').text('');
    $('#modalReceiptId').text('');
    $('#modalUserName').text('');
    $('#modalCar').text('');
    $('#modalOrderDate').text('');
    $('#modalReturnDate').text('');
    $('#modalCost').text('');
    $('#modalExtraFees').text('');
    $('#modalStatus').text('');

    // Clear the src attribute for the images
    $('#modalProofOfPayment img').attr('src', '');
    $('#proofOfPaymentFrontImage').attr('src', '');
    $('#proofOfPaymentBackImage').attr('src', '');
    $('#orderID-Verify').text('');

    $('#orderModal').modal('hide');
}

function setOrderVerify(isVerified) {
    var formData = new FormData();
    formData.append('orderId', $('#orderID-Verify').val());
    formData.append('orderIsVerified', isVerified);

    fetch('/Admin/OrderVerify', {
        method: 'POST',
        body: formData
    })
        .then(response => response.text().then(text => text ? JSON.parse(text) : {}))
        .then(data => {
            if (data.success) {
                //SUCCESS
                if (isVerified == true) {
                    // Insert your JS code here for successful verification
                    reloadActivePartialView("Order approved.");
                }
                else if (isVerified == false) {
                    // Insert your JS code here for denial
                    reloadActivePartialView("Order denied.");
                }
                else {
                    // Insert your JS code here for pending status
                    reloadActivePartialView("Order set to Pending.");
                }
            } else {
                //FAILED
                // Use this if you want to display the message from the controller
                // alert(data.message);
                alert("Something went wrong");
            }
            closeModalOrderDetails();
        })
        .catch(error => console.error('Error:', error));
}
