function updatePriceInput(value, inputId) {
    document.getElementById(inputId).value = `P${value}`;
}



//Listings MODAL
function closeModalEditListing() {
    $('#editListingModal').modal('hide');
}


function closeModalAddListing() {
    $('#addListingModal').modal('hide');
}
function openModalEditListing(listingID){
    fetch(`/Admin/GetListingDetails?listingId=${listingID}`)
        .then(response => response.text())
        .then(data => {
            // Check if the car was not found
            if (data.includes('"success":false')) {
                reloadActivePartialView(data.message);
                return;
            }

            document.getElementById('modalContainer').innerHTML = data;
            $('#editListingModal').modal('show');
        })
        .catch(error => console.error('Error:', error));
}

function openModalAddListing(){
    fetch(`/Admin/GetCarList`)
        .then(response => response.text())
        .then(data => {
            if (data.includes('"success":false')) {
                reloadActivePartialView(data.message);
                return;
            }
            document.getElementById('modalContainer').innerHTML = data;

            $('#addListingModal').modal('show');

        })
        .catch(error => console.error('Error:', error));
}
function addListing() {
    var carId = $('#addlistingCar').val();
    var hourlyPrice = $('#addListingHourlyRate').val();
    var dailyPrice = $('#addListingDailyRate').val();
    var weeklyPrice = $('#addListingWeeklyRate').val();
    var monthlyPrice = $('#addListingMonthlyRate').val();
    var startDate = $('#addListingStartDate').val();
    var endDate = $('#addListingEndDate').val();
    var details = $('#addListingDetails').val();

    if (!carId) {
        reloadActivePartialView("Please select a valid car");
        closeModalAddListing();
        return;
    }
    if (!startDate) {
        reloadActivePartialView("Please enter a valid starting date");
        closeModalAddListing();
        return;
    }

    if (!hourlyPrice || !dailyPrice || !weeklyPrice || !monthlyPrice) {
        reloadActivePartialView("Dont leave any of pricing fields empty.");
        closeModalAddListing();
        return;
    }

    var formData = new FormData();
    formData.append('listingaddCarID', carId);
    formData.append('listingaddHourlyPrice', hourlyPrice);
    formData.append('listingaddDailyPrice', dailyPrice);
    formData.append('listingaddWeeklyPrice', weeklyPrice);
    formData.append('listingaddMonthlyPrice', monthlyPrice);
    formData.append('listingaddStartDate', startDate);
    formData.append('listingsaddEndDate', endDate);
    formData.append('listingaddDetails', details);

    fetch('/Admin/AddListing', {
        method: 'POST',
        body: formData,
        processData: false,
        contentType: false
    })
        .then(response => response.text().then(text => text ? JSON.parse(text) : {}))
        .then(data => {
            if (data.success) {
                reloadActivePartialView("Listing successfully added.");
            } else {
                reloadActivePartialView(data.message);
            }
            closeModalAddListing();
        })
        .catch(error => console.error('Error:', error));
}

function editListing(listingId) {
    var carId = listingId;
    var hourlyPrice = $('#editListingHourlyRate').val();
    var dailyPrice = $('#editListingDailyRate').val();
    var weeklyPrice = $('#editListingWeeklyRate').val();
    var monthlyPrice = $('#editListingMonthlyRate').val();
    var status = $('#editlistingStatus').val();
    var details = $('#editlistingDetails').val();

    if (!hourlyPrice || !dailyPrice || !weeklyPrice || !monthlyPrice) {
        reloadActivePartialView("Dont leave any of pricing fields empty.");
        return;
    }

    var formData = new FormData();
    formData.append('listingeditID', carId);
    formData.append('listingeditHourlyPrice', hourlyPrice);
    formData.append('listingeditDailyPrice', dailyPrice);
    formData.append('listingeditWeeklyPrice', weeklyPrice);
    formData.append('listingeditMonthlyPrice', monthlyPrice);
    formData.append('listingeditStatus', status);
    formData.append('listingeditDetails', details);

    fetch('/Admin/EditListing', {
        method: 'POST',
        body: formData,
        processData: false,
        contentType: false
    })
        .then(response => response.text().then(text => text ? JSON.parse(text) : {}))
        .then(data => {
            if (data.success) {
                reloadActivePartialView("Listing successfully edited.");
            } else {
                reloadActivePartialView(data.message);
            }
            closeModalEditListing();
        })
        .catch(error => console.error('Error:', error));
}

function deleteListing(listingID) {
    fetch(`/Admin/DeleteListing?listingId=${listingID}`)
        .then(response => response.text().then(text => text ? JSON.parse(text) : {}))
        .then(data => {
            if (data.success) {
                closeModalEditListing();
                reloadActivePartialView("Listing successfully deleted.");
            } else {
                reloadActivePartialView(data.message);
            }
            closeModalEditListing();
        })
        .catch(error => console.error('Error:', error));
}