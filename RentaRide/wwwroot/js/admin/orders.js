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