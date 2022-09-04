const settings = {
    validClass: "is-valid",
    errorClass: "is-invalid"
};
$.validator.setDefaults(settings);
$.validator.unobtrusive.options = settings;

$('.btn-like').click(
    function (btn) {
        const id = btn.currentTarget.dataset.id;
        $.post(`/Home/AddAnimalLike/${id}`,
            function (data) {
                $(`#like_${id}`).html(data);
                btn.currentTarget.setAttribute('disabled', '');
            })
            .fail(function (error) {
                console.error(error.responseJSON.message);
            })
    }
);

const form = $('#newComment');
form.submit(function (e) {
    e.preventDefault();
    if (!form.valid()) return;
    const formdata = $(this).serializeArray()
    const data = {
        Comment: {
            AnimalId: this.dataset.id,
            Auther: formdata[0].value,
            Text: formdata[1].value
        }
    };
    $.post("/Home/AddAnimalComment/", data,
        function (data) {
            const comment = $(`
            <div class="card-body">
                <div class="card-title fs-5">${data.text}</div>
                    <div class="d-flex justify-content-between">
                        <small>Auther: ${data.auther}</small>
                        <small>Created At: ${data.createdDate}</small>
                    </div>
                </div>
            </div>
        `)
            $('#comments').prepend(comment);
            $(comment).hide();
            $(comment).slideDown();
            form.trigger('reset');
        })
        .fail(function (error) {
            console.error(error.responseJSON.message);
        })
});

$('.navbar-toggler').click(
    function (btn) {
        const isOpen = !btn.currentTarget.ariaExpanded;
        btn.currentTarget.ariaExpanded = isOpen;
        const menu = $(btn.currentTarget.dataset.target);
        menu.toggleClass('show', isOpen);
        menu.slideToggle(isOpen);
    }
);

$('#selectCategory').change(
    function () {
        $(this.parentNode).trigger('submit');
    }
);

$('.btn-delete').click(
    function (btn) {
        const id = btn.currentTarget.dataset.id;
        $.post('/Admin/DeleteAnimal/' + id,
            function (data) {
                const pet = $('#pet_' + id);
                pet.fadeOut('slow', function () { pet.remove(); });
            })
            .fail(function (error) {
                console.error(error.responseJSON.message);
            })
    }
);