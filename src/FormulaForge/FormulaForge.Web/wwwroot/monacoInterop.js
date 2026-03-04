window.monacoInterop = {
    editors: {},
    initialize: function (id, options, dotNetReference) {
        if (!window.require) {
            console.error('Monaco loader not found.');
            return;
        }

        require.config({ paths: { 'vs': '/lib/monaco-editor/vs' } });

        require(['vs/editor/editor.main'], () => {
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
    dispose: function (id) {
        const editor = this.editors[id];
        if (editor) {
            editor.dispose();
            delete this.editors[id];
        }
    }
};