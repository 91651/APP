import { Component, Vue, Prop , Inject} from 'vue-property-decorator';
import { State, Action, Getter } from 'vuex-class';
import './article.less';
import { ArticleModel, ArticleListModel, SearchArticleModel, Result } from '@/api-client/client';
import { _Article } from '@/api-client';

@Component
export default class ArticleComponent extends Vue {
  private $moment: any;
  private articles: Result<ArticleListModel[]> = new Result<ArticleListModel[]>();
  private serach: SearchArticleModel = new SearchArticleModel();

  private mounted() {
    this.getArticles();
  }
  private getArticles(){
    this.serach.take = this.serach.take || 10;
    this.serach.createdDate = this.serach.createdDate
     && new Date(this.$moment(this.serach.createdDate).format('YYYY-MM-DD'));
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
