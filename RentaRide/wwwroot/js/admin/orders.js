//function openModalAddOrder() {
//    fetch(`/Admin/GetOrderChoicesList`)
//        .then(response => response.text())
//        .then(data => {
//            if (data.includes('"success":false')) {
//                reloadActivePartialView(data.message);
//                return;
//            }



//            document.getElementById('modalOrderContainer').innerHTML = data;
//            const today = new Date();
//            var datePicker;

//            // Hide the start and end date pickers initially
//            const startPickerElement = document.querySelector(".flatpickr-orderstart");
//            const startDivElement = document.querySelector(".order-start-div");
//            const endPickerElement = document.querySelector(".flatpickr-orderend");
//            const endDivElement = document.querySelector(".order-end-div");
//            startPickerElement.style.display = 'none';
//            startDivElement.style.display = 'none';
//            endPickerElement.style.display = 'none';
//            endDivElement.style.display = 'none';

//            document.getElementById('addlistingCar').addEventListener('change', function () {
//                var listingId = this.value;
//                fetch(`api/API/GetListingDates/${listingId}`)
//                    .then(response => response.json())
//                    .then(data => {
//                        var disabledDates = [];
//                        data.orderDates.forEach(order => {
//                            var JSstartDeleteDate = new Date(order.startDate);
//                            var JSendDeleteDate = new Date(order.endDate);
//                            for (var d = new Date(JSstartDeleteDate); d <= JSendDeleteDate; d.setDate(d.getDate() + 1)) {
//                                disabledDates.push(new Date(d));
//                            }
//                            // Disable the day before the start date
//                            var dayBeforeStartDate = new Date(order.startDate);
//                            dayBeforeStartDate.setDate(dayBeforeStartDate.getDate() - 1);
//                            disabledDates.push(dayBeforeStartDate);
//                        });

//                        var DateMin = new Date(data.listingStartDate);
//                        if (DateMin < today) {
//                            DateMin.setDate(today.getDate());
//                        }

//                        datePicker = flatpickr("#addOrderDates", {
//                            mode: "range",
//                            dateFormat: "Y-m-dTH:i",
//                            enableTime: true,
//                            altInput: true,
//                            /*altFormat: "F j, Y, H:i \\(D\\)",*/
//                            altFormat: "F j, Y, H:i",
//                            minDate: DateMin,
//                            disable: disabledDates,
//                            minuteIncrement: 30,
//                            plugins: [new confirmDatePlugin({
//                                confirmIcon: "<i class='fa fa-check'></i>", // your icon's html, if you wish to override
//                                confirmText: "OK ",
//                                showAlways: false,
//                                theme: "light" // or "dark"
//                            })],
//                            onChange: function (selectedDates, dateStr, instance) {
//                                if (selectedDates.length >= 1) {
//                                    var StartDate = selectedDates[0];
//                                    var formattedStartDate = StartDate.toISOString().slice(0, 16);
//                                    var i = selectedDates.length;
//                                    $('#orderEnd').val();
//                                    if (selectedDates.length > 1) {
//                                        var EndDate = selectedDates[i - 1];
//                                        var formattedEndDate = EndDate.toISOString().slice(0, 16);
//                                    }
//                                }

//                                $('#orderStart').val(formattedStartDate);
//                                $('#orderEnd').val(formattedEndDate);
//                            }
//                        });

//                        datePicker.clear()
//                        if (data.listingEndDate != null) {
//                            var DateMax = new Date(data.listingEndDate);
//                            datePicker.set('maxDate', DateMax);
//                        }

//                    })

//                $('#addOrderModal').modal('show');

//        })
//        .catch(error => console.error('Error:', error));
//}

function openModalAddOrder() {
    fetch(`/Admin/GetOrderChoicesList`)
        .then(response => response.text())
        .then(data => {
            if (data.includes('"success":false')) {
                reloadActivePartialView(data.message);
                return;
            }

            document.getElementById('modalOrderContainer').innerHTML = data;

            const today = new Date();
            var startPicker, endPicker;

            // Hide the start and end date pickers initially
            const datePickerElement = document.querySelector(".flatpickr-orders");
            const dateDivElement = document.querySelector(".order-date-div");
            datePickerElement.style.display = 'none';
            dateDivElement.style.display = 'none';
            document.getElementById('addlistingCar').addEventListener('change', function () {
                var listingId = this.value;
                fetch(`api/API/GetListingDates/${listingId}`)
                    .then(response => response.json())
                        .then(data => {

                            var disabledDates = [];
                            data.orderDates.forEach(order => {
                                var JSstartDeleteDate = new Date(order.startDate);
                                var JSendDeleteDate = new Date(order.endDate);
                                for (var d = new Date(JSstartDeleteDate); d <= JSendDeleteDate; d.setDate(d.getDate() + 1)) {
                                    disabledDates.push(new Date(d));
                                }
                                // Disable the day before the start date
                                var dayBeforeStartDate = new Date(order.startDate);
                                dayBeforeStartDate.setDate(dayBeforeStartDate.getDate() - 1);
                                disabledDates.push(dayBeforeStartDate);
                            });

                            var DateMin = new Date(data.listingStartDate);
                            if (DateMin < today) {
                                DateMin.setDate(today.getDate());
                            }

                            datePicker = flatpickr("#addOrderDates", {
                                mode: "range",
                                dateFormat: "Y-m-dTH:i",
                                enableTime: true,
                                altInput: true,
                                /*altFormat: "F j, Y, H:i \\(D\\)",*/
                                altFormat: "F j, Y, H:i",
                                minDate: DateMin,
                                disable: disabledDates,
                                minuteIncrement: 30,
                                plugins: [new confirmDatePlugin({
                                    confirmIcon: "<i class='fa fa-check'></i>", // your icon's html, if you wish to override
                                    confirmText: "OK ",
                                    showAlways: false,
                                    theme: "light" // or "dark"
                                })],
                                onChange: function (selectedDates, dateStr, instance) {
                                    if (selectedDates.length >= 1) {
                                        var StartDate = selectedDates[0];
                                        var formattedStartDate = StartDate.toISOString().slice(0, 16);
                                        var i = selectedDates.length;
                                        $('#orderEnd').val();
                                        if (selectedDates.length > 1) {
                                            var EndDate = selectedDates[i - 1];
                                            var formattedEndDate = EndDate.toISOString().slice(0, 16);
                                        }
                                    }

                                    $('#addOrderStartDate').val(formattedStartDate);
                                    $('#addOrderEndDate').val(formattedEndDate);
                                }
                            });

                            datePicker.clear()
                            if (data.listingEndDate != null) {
                                var DateMax = new Date(data.listingEndDate);
                                datePicker.set('maxDate', DateMax);
                            }


                            datePickerElement.style.display = 'block';
                            dateDivElement.style.display = 'block';
                        })
                        .catch(error => console.error('Error:', error));
                });

            $('#addOrderModal').modal('show');
        })
        .catch(error => console.error('Error:', error));
}




function closeModalAddOrder() {
    $('#addOrderModal').modal('hide');
    $('#addOrderCostModal').modal('hide');
}

/* ---------------------------------------------------
    USER TAB SCRIPTS
----------------------------------------------------- */
function openModalOrderDetails(orderId) {
    fetch(`/Admin/GetOrderDetails?orderId=${orderId}`)
        .then(response => response.text())
        .then(data => {
            // Check if the car was not found
            if (data.includes('"success":false')) {
                reloadActivePartialView(data.message);
                return;
            }

            document.getElementById('modalOrderContainer').innerHTML = data;
            $('#orderModal').modal('show');
        })
        .catch(error => console.error('Error:', error));
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

function setOrderVerify(orderId, statusInt) {
    fetch(`/Admin/OrderVerify?orderId=${orderId}&statusInt=${statusInt}`)
        .then(response => response.text().then(text => text ? JSON.parse(text) : {}))
        .then(data => {
            // Check if the car was not found
            if (!data.success) {
                //FAILED
                // Use this if you want to display the message from the controller
                // alert(data.message);
                alert("Something went wrong");
                closeModalOrderDetails();
            }

            //SUCCESS
            if (statusInt == 2) {
                // Insert your JS code here for successful verification
                reloadActivePartialView("Order confirmed.");
            }
            else if (statusInt == 5) {
                // Insert your JS code here for denial
                reloadActivePartialView("Order denied.");
            }
            else {
                // Insert your JS code here for pending status
                reloadActivePartialView("Order set to Pending.");
            }
            closeModalOrderDetails();
        })
        .catch(error => console.error('Error:', error));
}

function openNextModal() {
    var listingId = $('#addlistingCar').val();
    fetch(`api/API/GetListingDetails/${listingId}`)
    .then(response => response.json())
    .then(data => {
        // Check if the listing was not found
        if (!data.success) {
            $('#addOrderModal').modal('hide');
            $('#addOrderModalLabel').modal('hide');
            reloadActivePartialViewError(data.message);
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
        reloadActivePartialViewError("Please select a valid listing.");
        closeModalAddOrder();
        return;
    }
    if (!userId) {
        reloadActivePartialViewError("Please select a user.");
        closeModalAddOrder();
        return;
    }

    if (!startDate || !endDate || !paymentId || !statusId || !Cost || !LocationLimit) {
        reloadActivePartialViewError("Dont leave any fields empty.");
        closeModalAddOrder();
        return;
    }

    if (startDate >= endDate) {
        reloadActivePartialViewError("Start Date cannot exceed the End Date");
        closeModalAddOrder();
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
                reloadActivePartialView("Order successfully added.");
            } else {
                reloadActivePartialViewError(data.message);
            }
            $('#addOrderModal').modal('hide');
            $('#addOrderCostModal').modal('hide');
            closeModalAddOrder();
            return;
        })
        .catch(error => console.error('Error:', error));
}

//END ORDER FUNCTIONS 
function openEndOrderModal(){
    $('#endOrderModal').modal('show');
}

function closeEndOrderModal(){
    $('#endOrderModal').modal('hide');
}

function submitEndOrder(){
    reloadActivePartialView("Order successfully ended.");
    removeBackdrops();
}

