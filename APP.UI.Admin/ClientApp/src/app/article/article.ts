import { Component, Vue, Prop } from 'vue-property-decorator';
import { State, Action, Getter } from 'vuex-class';
import './article.less';
import { ArticleModel, ArticleListModel, SearchArticleModel } from '@/api-client/client';
import { _Article } from '@/api-client';

@Component
export default class ArticleComponent extends Vue {
  private articles: ArticleListModel[] = new Array<ArticleListModel>();
  private serach: SearchArticleModel = new SearchArticleModel();

  private mounted() {
    _Article.getArticles(this.serach).then(r => { this.articles = r });
  }
  private pageChange(page: number) {
    this.serach.skip = page * 2;
  }
  private pageSizeChange(size: number) {
    debugger;
  }
}
