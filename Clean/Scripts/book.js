(function ($) {
    "use strict";

    var searchTimer = null;
    var currentBookId = null;

    function loadBookList(term) {
        $.get("/Book/Search", { term: term || "" }, function (html) {
            $("#bookListContainer").html(html);
            highlightSelectedBook();
        });
    }

    function loadBookDetail(bookId) {
        currentBookId = bookId;
        $.get("/Book/Detail/" + bookId, function (html) {
            $("#bookContentPanel").html(html);
            highlightSelectedBook();
        });
    }

    function loadSummary() {
        currentBookId = null;
        $.get("/Book/Summary", function (html) {
            $("#bookContentPanel").html(html);
            clearSelection();
        });
    }

    function loadCreateForm() {
        currentBookId = null;
        $.get("/Book/Create", function (html) {
            $("#bookContentPanel").html(html);
            clearSelection();
        });
    }

    function loadEditForm(bookId) {
        $.get("/Book/Edit/" + bookId, function (html) {
            $("#bookContentPanel").html(html);
        });
    }

    function highlightSelectedBook() {
        $(".book-list-item").removeClass("active");
        if (currentBookId) {
            $(".book-list-item[data-book-id='" + currentBookId + "']").addClass("active");
        }
    }

    function clearSelection() {
        $(".book-list-item").removeClass("active");
    }

    function getAntiForgeryToken() {
        return $("input[name='__RequestVerificationToken']").val();
    }

    // Search input with debounce
    $(document).on("input", "#bookSearchInput", function () {
        var term = $(this).val();
        clearTimeout(searchTimer);
        searchTimer = setTimeout(function () {
            loadBookList(term);
        }, 300);
    });

    // Add button
    $(document).on("click", "#bookAddButton", function () {
        loadCreateForm();
    });

    // Book list item click
    $(document).on("click", ".book-list-item", function () {
        var bookId = $(this).data("book-id");
        loadBookDetail(bookId);
    });

    // Edit button
    $(document).on("click", ".book-btn-edit", function () {
        var bookId = $(this).data("book-id");
        loadEditForm(bookId);
    });

    // Delete button
    $(document).on("click", ".book-btn-delete", function () {
        var bookId = $(this).data("book-id");
        if (!confirm("Are you sure you want to delete this book?")) {
            return;
        }

        $.ajax({
            url: "/Book/Delete",
            type: "POST",
            data: {
                id: bookId,
                __RequestVerificationToken: getAntiForgeryToken()
            },
            success: function (response) {
                if (response.success) {
                    var searchTerm = $("#bookSearchInput").val();
                    loadBookList(searchTerm);
                    loadSummary();
                }
            }
        });
    });

    // Form submit via AJAX
    $(document).on("submit", "#bookForm", function (e) {
        e.preventDefault();
        var $form = $(this);

        $.ajax({
            url: $form.attr("action"),
            type: "POST",
            data: $form.serialize(),
            success: function (response) {
                if (response.success) {
                    var searchTerm = $("#bookSearchInput").val();
                    loadBookList(searchTerm);
                    loadBookDetail(response.bookId);
                } else {
                    // Server returned validation errors as HTML
                    $("#bookContentPanel").html(response);
                }
            }
        });
    });

    // Cancel button
    $(document).on("click", "#bookFormCancel", function () {
        if (currentBookId) {
            loadBookDetail(currentBookId);
        } else {
            loadSummary();
        }
    });

})(jQuery);
