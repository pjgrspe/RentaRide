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
function openModalEditListing(listingID) {
    console.log("Opening edit modal for listing ID:", listingID);
    fetch(`/Admin/GetListingDetails?listingId=${listingID}`)
        .then(response => response.json())
        .then(data => {
            console.log("Response Data:", data);

            if (!data.success) {
                reloadActivePartialView(data.message);
                return;
            }

            // Populate the modal fields with the fetched data
            $('#editListingHourlyRate').val(data.hourlyRate);
            $('#editListingDailyRate').val(data.dailyRate);
            $('#editListingWeeklyRate').val(data.weeklyRate);
            $('#editListingMonthlyRate').val(data.monthlyRate);
            $('#editListingStartDate').val(new Date(data.startdate).toISOString().slice(0, 16));
            $('#editListingEndDate').val(data.enddate ? new Date(data.enddate).toISOString().slice(0, 16) : '');
            $('#editListingDetails').val(data.details);

            // Set the status dropdown
            $('#editListingStatus').val(data.status);

            // Update buttons with listing ID
            $('#deleteListingBtn').attr('onclick', `deleteListing(${listingID})`);
            $('#saveChangesBtn').attr('onclick', `editListing(${listingID})`);

            // Manually initialize the modal
            $('#editListingModal').modal('show');
            console.log("Modal should be shown now.");
        })
        .catch(error => {
            console.error('Error:', error);
            reloadActivePartialView("An error occurred while fetching the listing details.");
        });
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
                reloadActivePartialViewError(data.message);
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
        reloadActivePartialViewError("Dont leave any of pricing fields empty.");
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
                reloadActivePartialViewError(data.message);
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
                reloadActivePartialViewError(data.message);
            }
            closeModalEditListing();
        })
        .catch(error => console.error('Error:', error));
}