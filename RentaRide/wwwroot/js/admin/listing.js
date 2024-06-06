function updatePriceInput(value, inputId) {
    document.getElementById(inputId).value = `P${value}`;
}

//Listings MODAL
function openModalEditListing() {
    $('#editListingModal').modal('show');
}

function closeModalEditListing() {
    $('#editListingModal').modal('hide');
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

function closeModalAddListing() {
    $('#addListingModal').modal('hide');
}

function addListing() {
    var formData = new FormData();
    formData.append('listingaddCarID', $('#addlistingCar').val());
    formData.append('listingaddHourlyPrice', $('#addListingHourlyRate').val());
    formData.append('listingaddDailyPrice', $('#addListingDailyRate').val());
    formData.append('listingaddWeeklyPrice', $('#addListingWeeklyRate').val());
    formData.append('listingaddMonthlyPrice', $('#addListingMonthlyRate').val());
    formData.append('listingaddStartDate', $('#addListingStartDate').val());
    formData.append('listingsaddEndDate', $('#addListingEndDate').val());
    formData.append('listingaddDetails', $('#addListingDetails').val());

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
                reloadActivePartialView("Something went wrong.");
            }
            closeModalAddListing();
        })
        .catch(error => console.error('Error:', error));
}