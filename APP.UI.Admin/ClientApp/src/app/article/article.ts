import { Component, Vue, Prop } from 'vue-property-decorator';
import { State, Action, Getter } from 'vuex-class';
import './article.less';
import { ArticleModel, ArticleListModel, SearchArticleModel, Result } from '@/api-client/client';
import { _Article } from '@/api-client';

@Component
export default class ArticleComponent extends Vue {
  private articles: Result<ArticleListModel[]> = new Result<ArticleListModel[]>();
  private serach: SearchArticleModel = new SearchArticleModel();

  private mounted() {
    this.getArticles();
  }
  private getArticles(){
    this.serach.take = this.serach.take || 10;
    _Article.getArticles(this.serach).then(r => this.articles = r);
  }
  private pageChange(page: number) {
    this.serach.skip = --page * this.serach.take;
    this.getArticles();
  }
  private pageSizeChange(size: number) {
    this.serach.take = size;
    this.getArticles();
  }
}
