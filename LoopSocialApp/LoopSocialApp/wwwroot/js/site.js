function openPostDeleteConfirmation(postId) {
    UIkit.dropdown('.post-options-dropdown').hide();
    document.getElementById('deleteConfirmationPostId').value = postId;
    UIkit.modal('#postDeleteDialog').show();
}
