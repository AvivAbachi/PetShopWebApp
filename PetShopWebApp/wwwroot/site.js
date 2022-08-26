// Function Add Like
$('.btn-like').click(
    function (btn) {
        const id = btn.currentTarget.dataset.animal;
        $.post("/Home/AddAnimalLike/" + id, function (data) { $("#like_" + id).html(data); });
        btn.currentTarget.setAttribute('disabled', '');
    }
)
