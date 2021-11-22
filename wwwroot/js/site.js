function minMax(e) {
    const val = parseInt(e.data);
    if (val <= 5 && val > 0) {
        e.target.value = val;
    } else if (val < 1) {
        e.target.value = 1;
    } else {
        e.target.value = 5;
    }
}

function send(event, user, parameter) {
    if (event.key === "Enter") {
        console.log(user, parameter, event.target.value);
        $.ajax({
            type: "POST",
            url: "JSAddMarkAction",
            data: {
                userName: user,
                parameterName: parameter,
                mark: event.target.value
            }
        });
        event.target.value = "";
    }
}