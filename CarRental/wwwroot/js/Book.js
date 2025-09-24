
//// Calculate total amount when rental days change
//    document.getElementById('RentalDays').addEventListener('input', function() {
//        calculateTotal();
//});

//    // Calculate total amount when page loads
//    document.addEventListener('DOMContentLoaded', function() {
//        calculateTotal();
//});

//    function calculateTotal() {
//    const rentalDays = parseInt(document.getElementById('RentalDays').value) || 0;
//    const pricePerDay = @(vehicle?.PricePerDay ?? 0);
//    const total = rentalDays * pricePerDay;

//    document.getElementById('totalAmount').textContent = 'Rs. ' + total.toLocaleString();
//}

//    // Set minimum date to tomorrow
//    document.addEventListener('DOMContentLoaded', function() {
//    const tomorrow = new Date();
//    tomorrow.setDate(tomorrow.getDate() + 1);
//    const tomorrowStr = tomorrow.toISOString().split('T')[0];
//    document.getElementById('PickupDate').min = tomorrowStr;
//});

//    // Auth guard for Confirm Booking
//    document.getElementById('confirmBookingBtn').addEventListener('click', function () {
//    const isLoggedIn = @((ViewBag.IsCustomerLoggedIn ?? false) ? "true" : "false");
//    if (isLoggedIn === true || isLoggedIn === "true") {
//        document.getElementById('bookingForm').submit();
//    return;
//    }

//    const authModal = new bootstrap.Modal(document.getElementById('authRequiredModal'));
//    authModal.show();
//});
