$(() => {
    $("#photo-btn-input").change((event) => {
        var noPhoto = $(".no-photo");
        var imageContent = $(".image-content");

        console.log(event.target.files);

        if (!event.target.files || !event.target.files.length) {
            noPhoto.show();
            imageContent.hide();
            return;
        };

        var reader = new FileReader();

        reader.onload = (e) => {
            $("#photo").attr("src", e.target.result);
            noPhoto.hide();
            imageContent.show();
        };

        reader.readAsDataURL(event.target.files[0]);
    });
});