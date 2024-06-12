function openModalOrder(listingId, userId) {
    $('#CustomerOrderListingID').val(listingId);
    $('#CustomerOrderUserID').val(userId);
    const today = new Date();
    var datePicker;

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

            datePicker = flatpickr("#OrderDateSelect", {
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
                        $('#OrderFormEndDate').val();
                        if (selectedDates.length > 1) {
                            var EndDate = selectedDates[i - 1];
                            var formattedEndDate = EndDate.toISOString().slice(0, 16);
                        }
                    }

                    $('#CustomerOrderStartDate').val(formattedStartDate);
                    $('#CustomerOrderEndDate').val(formattedEndDate);
                }
            });

            datePicker.clear()
            if (data.listingEndDate != null) {
                var DateMax = new Date(data.listingEndDate);
                datePicker.set('maxDate', DateMax);
            }
            
            $('#orderModal').modal('show');
        })
        .catch(error => console.error('Error:', error));

}
function closeNextModalcustomer() {
    $('#addOrderCostModalcustomer').modal('hide');
    $('#orderModal').modal('show');
}
function openNextModalcustomer() {
    var FormListingId = $('#CustomerOrderListingID').val();
    var FormStartDate = $('#CustomerOrderStartDate').val();
    var FormEndDate = $('#CustomerOrderEndDate').val();
    var FormLocationLimit = $('#CustomerOrderLocationLimit').val();

    // Check if the start date, end date, and location limit inputs are not empty
    if (!FormStartDate || !FormEndDate) {
        alert('Please select start and end dates');
        return;
    }
    if (FormStartDate === FormEndDate) {
        alert('End Date cannot be equal to the start date');
        return;
    }
    if (!FormLocationLimit) {
        alert('Specify your location limit');
        return;
    }

    fetch(`api/API/GetListingDetails/${FormListingId}`)
        .then(response => response.json())
        .then(data => {
            // Check if the listing was not found
            if (!data.success) {
                $('#orderModal').modal('hide');
                $('#addOrderCostModalcustomer').modal('hide');
                return;
            }

            if (data.hourlyRate === 0) {
                $('#HourGroup').hide();
            }

            if (data.dailyRate === 0) {
                $('#DayGroup').hide();
            }

            if (data.weeklyRate === 0) {
                $('#WeekGroup').hide();
            }

            if (data.monthlyRate === 0) {
                $('#MonthGroup').hide();
            }

            // Update the cost computation modal with the rates from the listing
            $('#hourlyCost').text(data.hourlyRate);
            $('#dailyCost').text(data.dailyRate);
            $('#weeklyCost').text(data.weeklyRate);
            $('#monthlyCost').text(data.monthlyRate);

            // Calculate the difference between the start and end dates
            var startDate = new Date(FormStartDate);
            var endDate = new Date(FormEndDate);
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
            $('#CustomerOrderCost').val(totalCost);


            $('#orderModal').modal('hide');
            $('#addOrderCostModalcustomer').modal('show');
        })
        .catch(error => console.error('Error:', error));
}
function toggleProofOfPayment() {
    var paymentMethod = $('#CustomerOrderPaymentMethod').val();
    if (paymentMethod === '1') {
        $('#proofOfPaymentGroup').hide();
    } else {
        $('#proofOfPaymentGroup').show();
    }
}

function placeOrder() {
    var fromAdmin = false;
    var listingId = $('#CustomerOrderListingID').val();
    var userId = $('#CustomerOrderUserID').val();
    var startDate = $('#CustomerOrderStartDate').val();
    var endDate = $('#CustomerOrderEndDate').val();
    var Notes = $('#CustomerOrderNotes').val();
    var LocationLimit = $('#CustomerOrderLocationLimit').val();
    var withDriver = $('#CustomerOrderWithDriver').val();
    var driverId = null;

    var Cost = $('#CustomerOrderCost').val();
    var paymentId = $('#CustomerOrderPaymentMethod').val();
    var ProofOfPaymentIMG = $('#CustomerOrderPaymentProof')[0].files[0];
    var statusId = 3;
    var ExtraCost = 0;

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
    formData.append('orderaddFromAdmin', fromAdmin);
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
    formData.append('orderHasDriver', withDriver);



    fetch('/api/API/AddNewOrder', {
        method: 'POST',
        body: formData,
        processData: false,
        contentType: false
    })
        .then(response => response.text().then(text => text ? JSON.parse(text) : {}))
        .then(data => {
            if (data.status === "Success") {
                reloadActivePartialView("order successfully added.");
                alert(data.message);
                window.location.href = '/Customer/Index';
            } else {
                reloadActivePartialView(data.message);
                alert(data.message);
                window.location.href = '/Customer/Index';
            }
            $('#orderModal').modal('hide');
            $('#addOrderCostModalcustomer').modal('hide');
            closeModalAddOrder();
            return;
        })
            .catch(error => console.error('Error:', error));

}
