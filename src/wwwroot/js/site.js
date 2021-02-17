

//Prevent file attachments in Trix editor
document.addEventListener("trix-file-accept", function(event) {
    event.preventDefault();
})
