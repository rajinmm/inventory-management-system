export const setCursorRight = (input: HTMLInputElement | null) => {
  if (!input) return;
  const len = input.value.length;
  // setTimeout to allow browser default focus to happen first
  setTimeout(() => {
    try {
      input.setSelectionRange(len, len);
      input.focus();
    } catch (e) {
      // ignore if it fails on some environments
    }
  }, 0);
};
