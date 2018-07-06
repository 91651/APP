/*
bundleconfig.json 中不能添加注释，否则require出错；
bundleconfig.json 与 gulp 示例 https://github.com/madskristensen/BundlerMinifier/wiki/Use-from-Gulp
使用npm管理JavaScript包 http://www.cnblogs.com/hjklin/p/5883855.html
*/

"use strict";

var gulp = require("gulp"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    htmlmin = require("gulp-htmlmin"),
    uglify = require("gulp-uglify"),
    merge = require("merge-stream"),
    del = require("del"),
    path = require('path'),
    $ = require('gulp-load-plugins')(),
    bundleconfig = require("./bundleconfig.json"),
    requirefiles = require("./requirefiles.json");

gulp.task("min", ["min:js", "min:css", "min:html"]);

gulp.task("min:js", function () {
    var tasks = getBundles(".js").map(function (bundle) {
        return gulp.src(bundle.inputFiles, { base: "." })
            .pipe(concat(bundle.outputFileName))
            .pipe(uglify())
            .pipe(gulp.dest("."));
    });
    return merge(tasks);
});

gulp.task("min:css", function () {
    var tasks = getBundles(".css").map(function (bundle) {
        return gulp.src(bundle.inputFiles, { base: "." })
            .pipe(concat(bundle.outputFileName))
            .pipe(cssmin())
            .pipe(gulp.dest("."));
    });
    return merge(tasks);
});

gulp.task("min:html", function () {
    var tasks = getBundles(".html").map(function (bundle) {
        return gulp.src(bundle.inputFiles, { base: "." })
            .pipe(concat(bundle.outputFileName))
            .pipe(htmlmin({ collapseWhitespace: true, minifyCSS: true, minifyJS: true }))
            .pipe(gulp.dest("."));
    });
    return merge(tasks);
});

gulp.task("clean", function () {
    var files = bundleconfig.map(function (bundle) {
        return bundle.outputFileName;
    });

    return del(files);
});

gulp.task("watch", function () {
    getBundles(".js").forEach(function (bundle) {
        gulp.watch(bundle.inputFiles, ["min:js"]);
    });

    getBundles(".css").forEach(function (bundle) {
        gulp.watch(bundle.inputFiles, ["min:css"]);
    });

    getBundles(".html").forEach(function (bundle) {
        gulp.watch(bundle.inputFiles, ["min:html"]);
    });
});

function getBundles(extension) {
    return bundleconfig.filter(function (bundle) {
        return new RegExp(`${extension}$`).test(bundle.outputFileName);
    });
}

gulp.task('fonts', function () {
    return gulp.src(requirefiles.Font.inputFiles)
        .pipe(gulp.dest(requirefiles.Font.outputFolder));
});
gulp.task('JsPlugins', function () {
    return gulp.src(requirefiles.JsPlugins.inputFiles)
        .pipe(gulp.dest(requirefiles.JsPlugins.outputFolder));
});
gulp.task("dev", ["min:js", "min:css", "min:html", "fonts", "JsPlugins"]);
