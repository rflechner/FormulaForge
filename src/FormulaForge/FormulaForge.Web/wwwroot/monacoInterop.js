window.monacoInterop = {
    editors: {},
    initialize: function (id, options, dotNetReference) {
        if (!window.require) {
            console.error('Monaco loader not found.');
            return;
        }

        require.config({ paths: { 'vs': '/lib/monaco-editor/vs' } });

        require(['vs/editor/editor.main'], () => {
            // Load language contributions for highlighting
            require(['vs/basic-languages/monaco.contribution', 'vs/language/typescript/monaco.contribution'], () => {
                const container = document.getElementById(id);
                if (!container) {
                    console.error(`Container for editor with id ${id} not found.`);
                    return;
                }

                const editor = monaco.editor.create(container, {
                    value: options.value || '',
                    language: options.language || 'javascript',
                    automaticLayout: options.automaticLayout ?? true,
                    theme: options.theme || 'vs-dark'
                });

                this.editors[id] = editor;

                editor.onDidBlurEditorText(() => {
                    dotNetReference.invokeMethodAsync('OnBlur');
                });

                dotNetReference.invokeMethodAsync('NotifyInitialized');
            });
        });
    },
    getValue: function (id) {
        const editor = this.editors[id];
        return editor ? editor.getValue() : '';
    },
    setValue: function (id, value) {
        const editor = this.editors[id];
        if (editor) {
            editor.setValue(value);
        }
    },

    highlightError: function (id, markers) {
        const editor = this.editors[id];
        if (!editor) return;

        const model = editor.getModel();
        if (!model) return;

        // Convertir les marqueurs au format Monaco
        const monacoMarkers = markers.map(m => ({
            severity: monaco.MarkerSeverity.Error,
            message: m.message || 'Error',
            startLineNumber: m.range.start.line,
            startColumn: m.range.start.column,
            endLineNumber: m.range.end.line,
            endColumn: m.range.end.column
        }));

        // Appliquer les marqueurs au modèle
        monaco.editor.setModelMarkers(model, 'formulaforge', monacoMarkers);
    },

    clearErrorHighlights: function (id) {
        const editor = this.editors[id];
        if (!editor) return;

        const model = editor.getModel();
        if (!model) return;

        // Effacer tous les marqueurs
        monaco.editor.setModelMarkers(model, 'formulaforge', []);
    },

    dispose: function (id) {
        const editor = this.editors[id];
        if (editor) {
            editor.dispose();
            delete this.editors[id];
        }
    }
};