import { Component, Vue, Prop } from 'vue-property-decorator';
import { State, Action, Getter } from 'vuex-class';
import './article.less';
import { ArticleModel, ArticleListModel } from '@/api-client/client';
import { _Article } from '@/api-client';

@Component
export default class ArticleComponent extends Vue {
  private articles!: ArticleListModel[] = new Array<ArticleListModel>();

  private mounted() {
    _Article.getArticles().then(r => { this.articles = r });
  }
  private pageChange(page: number) {
    debugger;
  }
  private pageSizeChange(size: number) {
    debugger;
  }
}
