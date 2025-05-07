async function add(bookId) {
    var usernameEl = document.getElementById("username");
    if (usernameEl == null) {
        window.location.href = "/Identity/Account/Login";
        return;
    }

    try {
        var response = await fetch(`/Cart/AddItem?bookId=${bookId}`);
        if (response.status == 200) {
            var result = await response.json();

            // Update cart count
            var cartCountEl = document.getElementById("cartCount");
            cartCountEl.innerHTML = result;

            // Show simple notification
            showSimpleNotification();
        }
    }
    catch (err) {
        console.log(err);
    }
}

function showSimpleNotification() {
    const notification = document.getElementById("simpleNotification");
    notification.classList.remove("notification-hidden");
    notification.classList.add("notification-show");

    // Reset animation after 3 seconds
    setTimeout(() => {
        notification.classList.remove("notification-show");
        notification.classList.add("notification-hidden");
    }, 3000);
}