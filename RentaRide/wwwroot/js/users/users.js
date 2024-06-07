function openModalOrder(listingId, userId,) {
    $('#OrderFormListingID').val(listingId);
    $('#OrderFormUserID').val(userId);
    const today = new Date();
    var startPicker, endPicker;

    fetch(`/Customer/GetListingDates?listingId=${listingId}`)
        .then(response => response.json())
        .then(data => {
            var disabledDates = [];
            data.orderDates.forEach(order => {
                var startDate = new Date(order.startDate);
                var endDate = new Date(order.endDate);
                for (var d = new Date(startDate); d <= endDate; d.setDate(d.getDate() + 1)) {
                    disabledDates.push(new Date(d));
                }
                // Disable the day before the start date
                var dayBeforeStartDate = new Date(startDate);
                dayBeforeStartDate.setDate(startDate.getDate() - 1);
                disabledDates.push(dayBeforeStartDate);
            });

            var listingStartDate = new Date(data.listingStartDate);
            if (listingStartDate < today) {
                listingStartDate = today;
            }

            var listingEndDate = new Date(data.listingEndDate);

            // Initialize Flatpickr for the "Order Start Date" field
            startPicker = flatpickr(".flatpickr-orderstart", {
                dateFormat: "Y-m-dTH:i",
                enableTime: true,
                altInput: true,
                altFormat: "F j, Y, H:i",
                minDate: listingStartDate,
                maxDate: listingEndDate,
                allowInput: true,
                disable: disabledDates,
                onChange: function (selectedDates, dateStr, instance) {
                    if (selectedDates.length > 0) {
                        var selectedStartDate = selectedDates[0];
                        var minEndDate = new Date(selectedStartDate.getTime() + 1 * 60 * 60 * 1000); // At least 1 hour ahead

                        var maxEndDate = listingEndDate;
                        for (var i = 0; i < disabledDates.length; i++) {
                            if (disabledDates[i] > selectedStartDate) {
                                maxEndDate = new Date(disabledDates[i].getTime() - 1 * 60 * 60 * 1000);
                                break;
                            }
                        }

                        endPicker.set('minDate', minEndDate);
                        endPicker.set('maxDate', maxEndDate);

                        // Automatically select the earliest available date for endPicker
                        var earliestEndDate = new Date(minEndDate);
                        while (disabledDates.some(d => d.getTime() === earliestEndDate.getTime())) {
                            earliestEndDate.setDate(earliestEndDate.getDate() + 1);
                        }

                        endPicker.setDate(earliestEndDate, true);
                    }
                }
            });

            // Initialize Flatpickr for the "Order End Date" field
            endPicker = flatpickr(".flatpickr-orderend", {
                dateFormat: "Y-m-dTH:i",
                enableTime: true,
                altInput: true,
                altFormat: "F j, Y, H:i",
                allowInput: true,
                disable: disabledDates
            });

            var earliestAvailableDate = new Date(Math.max(startPicker.config.minDate.getTime(), today.getTime()));
            while (disabledDates.some(d => d.getTime() === earliestAvailableDate.getTime())) {
                earliestAvailableDate.setDate(earliestAvailableDate.getDate() + 1);
            }

            // Set the selected dates to the earliest available date
            startPicker.setDate(earliestAvailableDate, true);
            startPicker.set('default', earliestAvailableDate);

            var initialEndDate = new Date(earliestAvailableDate.getTime() + 1 * 60 * 60 * 1000); // Add one hour
            while (disabledDates.some(d => d.getTime() === initialEndDate.getTime())) {
                initialEndDate.setDate(initialEndDate.getDate() + 1);
            }
            endPicker.setDate(initialEndDate, true);

        })
        .catch(error => console.error('Error:', error));

    $('#orderModal').modal('show');
}

//function openModalOrder() {
//    $('#orderModal').modal('show');
//}

function closeModalOrder() {
    reloadActivePartialView(data.message);
    $('#orderModal').modal('hide');
    window.location.href = '/Customer/Index';
}
function closeNextModalcustomer() {
    $('#orderModal').modal('show');
    $('#addOrderCostModalcustomer').modal('hide')
}

function openNextModalcustomer() {
    var listingId = $('#OrderFormListingID').val
    var startDate = $('#OrderStartDate').val();
    var endDate = $('#OrderEndDate').val();
    var locationLimit = $('#locationLimit').val();

    // Check if the start date, end date, and location limit inputs are not empty
    if (!startDate || !endDate) {
        alert('Please select start and and dates');
        return;
    }
    if (!startDate || !endDate || !locationLimit) {
        alert('Please fill in your location limit');
        return;
    }

    fetch(`/Customer/GetListingDetails?listingId=${listingId}`)
        .then(response => response.json())
        .then(data => {
            // Check if the listing was not found
            if (!data.success) {
                $('#orderModal').modal('hide');
                $('#addOrderCostModalcustomer').modal('hide');
                return;
            }

            // Update the cost computation modal with the rates from the listing
            $('#hourlyCost').text(data.hourlyRate);
            $('#dailyCost').text(data.dailyRate);
            $('#weeklyCost').text(data.weeklyRate);
            $('#monthlyCost').text(data.monthlyRate);

            // Calculate the difference between the start and end dates
            var startDate = new Date($('#OrderStartDate').val());
            var endDate = new Date($('#OrderEndDate').val());
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
            var totalCost = (Math.floor(remainingHours) * data.hourlyRate) + (Math.floor(remainingDays) * data.dailyRate) + (Math.floor(remainingWeeks) * data.weeklyRate) + (Math.floor(totalMonths) * data.monthlyRate);

            $('#TotalCostOrder').text(totalCost);


            $('#orderModal').modal('hide');
            $('#addOrderCostModalcustomer').modal('show');
        })
        .catch(error => console.error('Error:', error));
}


function placeOrder() {
    var listingId = $('#OrderFormListingID').val();
    var userId = $('#OrderFormUserID').val();
    var withDriver = $('#withDriver').val();
    var driverId = null;
    if (withDriver === 'true') {
        driverId = $('#addlistingDriver').val();
    }
    var startDate = $('#OrderStartDate').val();
    var endDate = $('#OrderEndDate').val();
    var paymentId = $('#paymentMethod').val();
    var statusId = 3;
    var ProofOfPaymentIMG = $('#proofOfPayment')[0].files[0];
    var Cost = $('#addOrderTotalCost').val();
    var ExtraCost = 0;
    var LocationLimit = $('#locationLimit').val();
    var Notes = $('#TotalCostOrder').text();

    if (!listingId) {
        reloadActivePartialView("Please select a valid listing");
        closeModalAddOrder();
        return;
    }
    if (!userId) {
        reloadActivePartialView("Please select a user");
        closeModalAddOrder();
        return;
    }

    if (!startDate || !endDate || !paymentId || !statusId || !Cost || !LocationLimit) {
        reloadActivePartialView("Dont leave any fields empty.");
        closeModalAddOrder();
        return;
    }

    if (startDate >= endDate) {
        reloadActivePartialView("Start Date cannot exceed the End Date");
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
