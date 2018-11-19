import { Component, Vue, Prop } from 'vue-property-decorator';
import { mavonEditor } from 'mavon-editor';
import 'mavon-editor/dist/css/index.css'
import { quillEditor } from 'vue-quill-editor';
import 'quill/dist/quill.core.css';
import 'quill/dist/quill.snow.css';

import './article.less';
import {
  ArticleModel, ArticleListModel, SearchArticleModel,
  Cascader, Result, ChannelModel
} from '@/api-client/client';
import { _Article } from '@/api-client';

@Component({
  components: {
    mavonEditor,
    quillEditor
  },
})

export default class ArticleComponent extends Vue {
  private $moment: any;
  private articleForm: any = { showDrawer: false, title: '', disabledEditorSwitch: false, disabledEditorTooltip: true };
  private articles: Result<ArticleListModel[]> = new Result<ArticleListModel[]>();
  private serach: SearchArticleModel = new SearchArticleModel();
  private article: ArticleModel = new ArticleModel();
  private channel: ChannelModel = new ChannelModel();
  private channels: Cascader[] = new Array<Cascader>();
  private quillOptions = {
    placeholder: '开始编辑...',
  };
  private articleFormRules: any = {
    title: [{ required: true, trigger: 'blur', message: '请输入文章标题' }],
    _channel: [{ required: true, trigger: 'on-visible-change', type: "array", message: '请选择文章栏目' }]
  };

  private mounted() {
    this.getArticles();
  }
  private getArticles() {
    this.serach.take = this.serach.take || 10;
    this.serach.createdDate = this.serach.createdDate
      && new Date(this.$moment(this.serach.createdDate).format('YYYY-MM-DD'));
    _Article.getArticles(this.serach).then(r => this.articles = r);
  }
  private editArticle(article: ArticleModel) {
    this.articleForm.showDrawer = true;
    this.articleForm.title = article || '创建文章';
    this.getChannels();
  }
  private getChannels() {
    _Article.getChannelsToCascader('').then(r => this.channels = r);
  }
  private getChildrenChannels(item: any, callback: any) {
    item.loading = true;
    _Article.getChannelsToCascader(item.value)
      .then(r => { item.children = r; item.loading = false; callback(); });
  }
  private addChannel() {
    if (this.channel) {
      this.channel.parentId = this.article.channelId;
      _Article.addChannel(this.channel).then(r => {
        let channels = (this.article as any)._channel;
        let currItem = this.findCascaderItemByValue(channels);
        this.getChildrenChannels(currItem, () => {
          channels.push(r);
        });
      });
    }
  }
  private articleCascaderChange(selected: any) {
    this.article.channelId = selected && selected[selected.length - 1];
    (this.article as any)._channel = selected;
  }
  private findCascaderItemByValue(values: string[]) {
    let channels = this.channels;
    let item: Cascader = new Cascader();
    values.forEach(f => {
      if (values.indexOf(f) === 0) {
        item = channels.filter(m => m.value === f)[0]
      } else {
        if (values.indexOf(f) === values.length - 1) {
          item = item.children && item.children.filter(m => m.value === f)[0] || new Cascader();
        } else {
          item = item.children && item.children.filter(m => m.value === f)[0] || new Cascader();
        }
      }
    });
    return item;
  }
  private editorSwitch(isDisabled: boolean) {
    this.articleForm.disabledEditorSwitch = isDisabled;
    this.articleForm.disabledEditorTooltip = !isDisabled;
  }
  private mavonEditorChange(value: string) {
    this.editorSwitch(!!value);
  }

  private quillEditorChange(value: string) {
    this.editorSwitch(!!value);
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
