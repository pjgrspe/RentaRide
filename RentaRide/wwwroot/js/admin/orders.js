function openModalAddOrder() {
    $('#addOrderModal').modal('show');
}

function closeModalAddOrder() {
    $('#addOrderModal').modal('hide');
}
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