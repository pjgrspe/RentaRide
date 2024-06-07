function openModalAddOrder() {
    fetch(`/Admin/GetOrderChoicesList`)
        .then(response => response.text())
        .then(data => {
            if (data.includes('"success":false')) {
                reloadActivePartialView(data.message);
                return;
            }

            document.getElementById('modalOrderContainer').innerHTML = data;

            // Calculate the date 18 years ago from today
            const today = new Date();
            var tomorrow = new Date();
            tomorrow.setDate(tomorrow.getDate() + 1);

            var endPicker = flatpickr(".flatpickr-orderend", {
                dateFormat: "Y-m-dTH:i",
                enableTime: true,
                altInput: true,
                altFormat: "F j, Y, H:i",
                allowInput: true,
                defaultDate: tomorrow,
            });

            // Initialize Flatpickr for the "Order Start Date" and "Order End Date" fields
            var startPicker = flatpickr(".flatpickr-orderstart", {
                dateFormat: "Y-m-dTH:i",
                enableTime: true,
                altInput: true,
                altFormat: "F j, Y, H:i",
                minDate: "today",
                allowInput: true,
                defaultDate: today,
                onChange: function (selectedDates, dateStr, instance) {
                    if (selectedDates.length > 0) {
                        endPicker.set('minDate', selectedDates[0]);
                        tomorrow.setDate(selectedDates[0].getDate() + 1);
                        endPicker.setDate(tomorrow, true);
                    }
                }
            });

            document.getElementById('addlistingCar').addEventListener('change', function () {
                var listingId = this.value;

                fetch(`/Admin/GetListingDates?listingId=${listingId}`)
                    .then(response => response.json())
                    .then(data => {
                        var disabledDates = [];
                        data.orderDates.forEach(order => {
                            var startDate = new Date(order.startDate);
                            var endDate = new Date(order.endDate);
                            for (var d = startDate; d <= endDate; d.setDate(d.getDate() + 1)) {
                                disabledDates.push(new Date(d));
                            }
                        });

                        var listingStartDate = new Date(data.listingStartDate);
                        if (listingStartDate < today) {
                            listingStartDate = today;
                        }

                        startPicker.set('minDate', listingStartDate);
                        if (data.listingEndDate) {
                            startPicker.set('maxDate', new Date(data.listingEndDate));
                        }
                        startPicker.set('disable', disabledDates);

                        endPicker.set('minDate', listingStartDate.getDate() + 1);
                        if (data.listingEndDate) {
                            endPicker.set('maxDate', new Date(data.listingEndDate));
                        }
                        endPicker.set('disable', disabledDates);

                        var earliestAvailableDate = new Date(startPicker.config.minDate);
                        while (disabledDates.some(d => d.getFullYear() === earliestAvailableDate.getFullYear() && d.getMonth() === earliestAvailableDate.getMonth() && d.getDate() === earliestAvailableDate.getDate())) {
                            earliestAvailableDate.setDate(earliestAvailableDate.getDate() + 1);
                        }

                        // Set the selected dates to the earliest available date
                        startPicker.setDate(earliestAvailableDate, true);
                        startPicker.set('default', earliestAvailableDate);
                        endPicker.setDate(new Date(earliestAvailableDate.getTime() + 24 * 60 * 60 * 1000), true); // Add one day to the earliest available date
                    })
                    .catch(error => console.error('Error:', error));
            });

            $('#addOrderModal').modal('show');
        })
        .catch(error => console.error('Error:', error));

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
        var endDate = new Date($('#addOrderEndDate').val());
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

        $('#addOrderTotalCost').val(totalCostSuggestion);


        $('#addOrderModal').modal('hide');
        $('#addOrderCostModal').modal('show');
    })
    .catch(error => console.error('Error:', error));
}

function closeNextModal() {
    $('#addOrderModal').modal('show');
    $('#addOrderCostModal').modal('hide');
}

function addOrder() {
    var listingId = $('#addlistingCar').val();
    var userId = $('#addlistingUser').val();
    var driverId = $('#addlistingDriver').val();
    var startDate = $('#addOrderStartDate').val();
    var endDate = $('#addOrderEndDate').val();
    var paymentId = $('#addlistingPaymentMethod').val();
    var statusId = $('#addlistingPaymentStatus').val();
    var ProofOfPaymentIMG = $('#addOrderProofOfPayment')[0].files[0];
    var Cost = $('#addOrderTotalCost').val();
    var ExtraCost = $('#addOrderExtraFees').val();
    var LocationLimit = $('#addOrderLocLimit').val();
    var Notes = $('#addOrderNotes').val();

    if (!listingId) {
        reloadActivePartialView("Please select a valid listing");
        closeModalAddListing();
        return;
    }
    if (!userId) {
        reloadActivePartialView("Please select a user");
        closeModalAddListing();
        return;
    }

    if (!startDate || !endDate || !paymentId || !statusId || !Cost || !LocationLimit) {
        reloadActivePartialView("Dont leave any fields empty.");
        closeModalAddListing();
        return;
    }

    if (startDate >= endDate) {
        reloadActivePartialView("Start Date cannot exceed the End Date");
        closeModalAddListing();
        return;

    }

    var formData = new FormData();
    formData.append('orderaddFromAdmin', true);
    formData.append('orderaddListingID', listingId);
    formData.append('orderaddUserID', userId);
    formData.append('orderaddDriverID', driverId);
    formData.append('orderaddStart', startDate);
    formData.append('orderaddEnd', endDate);
    formData.append('orderaddPaymentID', paymentId);
    formData.append('orderaddStatusID', statusId);
    formData.append('orderaddPaymentIMG', ProofOfPaymentIMG);
    formData.append('orderaddCost', Cost);
    formData.append('orderaddExtraFee', ExtraCost);
    formData.append('orderaddLocationLimit', LocationLimit);
    formData.append('orderaddNotes', Notes);



    fetch('/Admin/AddNewOrder', {
        method: 'POST',
        body: formData,
        processData: false,
        contentType: false
    })
        .then(response => response.text().then(text => text ? JSON.parse(text) : {}))
        .then(data => {
            if (data.success) {
                reloadActivePartialView("order successfully added.");
            } else {
                reloadActivePartialView(data.message);
            }
            $('#addOrderModal').modal('hide');
            $('#addOrderCostModal').modal('hide');
            closeModalAddOrder();
            return;
        })
        .catch(error => console.error('Error:', error));
}
