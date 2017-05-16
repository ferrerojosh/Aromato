import { Aromato.ClientPage } from './app.po';

describe('aromato.client App', () => {
  let page: Aromato.ClientPage;

  beforeEach(() => {
    page = new Aromato.ClientPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
