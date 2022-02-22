window.CKEditorInterop = (() => {
    const editors = {};

    return {
        init(id, dotNetReference) {
            ClassicEditor
                .create(document.getElementById(id), {
                    toolbar: {
                        items: [
                            'heading',
                            '|',
                            'bold',
                            'italic',
                            'link',
                            'bulletedList',
                            'numberedList',
                            '|',
                            'outdent',
                            'indent',
                            '|',
                            'imageUpload',
                            'imageInsert',
                            'blockQuote',
                            'insertTable',
                            'mediaEmbed',
                            '|',
                            'undo',
                            'redo',
                            '|',
                            'alignment',
                            'findAndReplace',
                            'fontBackgroundColor',
                            'fontColor',
                            'fontFamily',
                            'fontSize',
                            'horizontalLine',
                            '|',
                            'highlight',
                            'specialCharacters',
                            'underline',
                            'sourceEditing',
                            'codeBlock'
                        ]
                    },
                    language: 'en',
                    image: {
                        toolbar: [
                            'imageTextAlternative',
                            'imageStyle:inline',
                            'imageStyle:block',
                            'imageStyle:side',
                            'linkImage'
                        ]
                    },
                    table: {
                        contentToolbar: [
                            'tableColumn',
                            'tableRow',
                            'mergeTableCells',
                            'tableCellProperties',
                            'tableProperties'
                        ]
                    }
                })
                .then(editor => {
                    editors[id] = editor;
                    editor.model.document.on('change:data', () => {
                        let data = editor.getData();
                        const el = document.createElement('div');
                        el.innerHTML = data;
                        if (el.innerText.trim() == '')
                            data = null;
                        dotNetReference.invokeMethodAsync('EditorDataChanged', data);
                    });
                })
                .catch(error => console.error(error));
        },
        destroy(id) {
            editors[id].destroy()
                .then(() => delete editors[id])
                .catch(error => console.log(error));
        }
    };
})();


var chart1 = {

    init: function (model) {
        console.log(model)

        var e, t, a, o, s, r, i, l, n, c, d, h;
        a = document.getElementById("kt_charts_widget_1_chart"),
            o = 300,
            s = "#A1A5B7",
            r = "#EFF2F5",
            i = "#009EF7",
            l = "#E4E6EF",
            a && new ApexCharts(a, {
                series: [{
                    name: "Net Profit",
                    data: model
                }, {
                    name: "Revenue",
                    data: [76, 85, 101, 98, 87, 105]
                }],
                chart: {
                    fontFamily: "inherit",
                    type: "bar",
                    height: o,
                    toolbar: {
                        show: !1
                    }
                },
                plotOptions: {
                    bar: {
                        horizontal: !1,
                        columnWidth: ["30%"],
                        borderRadius: 4
                    }
                },
                legend: {
                    show: !1
                },
                dataLabels: {
                    enabled: !1
                },
                stroke: {
                    show: !0,
                    width: 2,
                    colors: ["transparent"]
                },
                xaxis: {
                    categories: ["Feb", "Mar", "Apr", "May", "Jun", "Jul"],
                    axisBorder: {
                        show: !1
                    },
                    axisTicks: {
                        show: !1
                    },
                    labels: {
                        style: {
                            colors: s,
                            fontSize: "12px"
                        }
                    }
                },
                yaxis: {
                    labels: {
                        style: {
                            colors: s,
                            fontSize: "12px"
                        }
                    }
                },
                fill: {
                    opacity: 1
                },
                states: {
                    normal: {
                        filter: {
                            type: "none",
                            value: 0
                        }
                    },
                    hover: {
                        filter: {
                            type: "none",
                            value: 0
                        }
                    },
                    active: {
                        allowMultipleDataPointsSelection: !1,
                        filter: {
                            type: "none",
                            value: 0
                        }
                    }
                },
                tooltip: {
                    style: {
                        fontSize: "12px"
                    },
                    y: {
                        formatter: function (e) {
                            return "$" + e + " thousands"
                        }
                    }
                },
                colors: [i, l],
                grid: {
                    borderColor: r,
                    strokeDashArray: 4,
                    yaxis: {
                        lines: {
                            show: !0
                        }
                    }
                }
            }).render();
    }
}



