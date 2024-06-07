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
=======
function openNextModal(listingID) {
    fetch(`/Admin/GetListingDetails?listingId=${listingID}`)
    .then(response => response.json())
    .then(data => {
        // Check if the listing was not found
        if (!data.success) {
            $('#addOrderModal').modal('hide');
            $('#addOrderCostModal').modal('hide');
            reloadActivePartialView(data.message);
            return;
        }

        // Update the cost computation modal with the rates from the listing
        $('#hourlyCost').text(data.hourlyRate);
        $('#dailyCost').text(data.dailyRate);
        $('#weeklyCost').text(data.weeklyRate);
        $('#monthlyCost').text(data.monthlyRate);

        // Calculate the difference between the start and end dates
        var startDate = new Date($('#addOrderStartDate').val());
        var endDate = new Date($('#addListingEndDate').val());
        var diffInMilliseconds = endDate - startDate;


        // Convert the difference to hours, days, weeks, and months
        var totalHours = diffInMilliseconds / (1000 * 60 * 60);
        var totalDays = Math.floor(totalHours / 24);
        var totalMonths = Math.floor(totalDays / 30); // Approximation

        // Calculate the remaining days, weeks, and hours
        var remainingDays = totalDays % 30;
        var remainingWeeks = Math.floor(remainingDays / 7);
        remainingDays %= 7;
        var remainingHours = totalHours % 24;

        // Update the modal with the calculated values
        $('#HoursCalc').text(Math.floor(remainingHours));
        $('#DaysCalc').text(Math.floor(remainingDays));
        $('#WeeksCalc').text(Math.floor(remainingWeeks));
        $('#MonthsCalc').text(Math.floor(totalMonths));

        // Cost Display
        $('#HourCost').text(Math.floor(remainingHours) * data.hourlyRate);
        $('#DayCost').text(Math.floor(remainingDays) * data.dailyRate);
        $('#WeekCost').text(Math.floor(remainingWeeks) * data.weeklyRate);
        $('#MonthCost').text(Math.floor(totalMonths) * data.monthlyRate);

        // Calculate the total cost suggestion
        var totalCostSuggestion = (Math.floor(remainingHours) * data.hourlyRate) + (Math.floor(remainingDays) * data.dailyRate) + (Math.floor(remainingWeeks) * data.weeklyRate) + (Math.floor(totalMonths) * data.monthlyRate);

        $('#addOrderTotalCostToo').val(totalCostSuggestion);


        $('#addOrderModal').modal('hide');
        $('#addOrderCostModal').modal('show');
    })
    .catch(error => console.error('Error:', error));
}

function closeNextModal() {
    $('#addOrderModal').modal('show');
    $('#addOrderCostModal').modal('hide');
}
