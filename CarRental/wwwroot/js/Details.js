
//// Intercept Book Now when not logged in
//    document.addEventListener('DOMContentLoaded', function() {
//    const bookBtn = document.getElementById('bookNowBtn');
//    if (!bookBtn) return;

//    const isLoggedIn = @((ViewBag.IsCustomerLoggedIn ?? false) ? "true" : "false");
//    if (isLoggedIn === true || isLoggedIn === "true") {
//        return; // allow normal navigation
//    }

//    bookBtn.addEventListener('click', function (e) {
//        e.preventDefault();
//    const modal = new bootstrap.Modal(document.getElementById('authRequiredModal'));
//    modal.show();
//    });
//});
